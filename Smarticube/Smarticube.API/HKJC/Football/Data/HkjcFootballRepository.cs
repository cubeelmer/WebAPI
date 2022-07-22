using Microsoft.EntityFrameworkCore;
using Smarticube.API.HKJC.Football.Models;
using Smarticube.API.Utils;

namespace Smarticube.API.HKJC.Football.Data
{
    public class HkjcFootballRepository : IHkjcFootballRepository
    {
        private readonly HkjcFootballDbContext _context;                
        private bool disposedValue;
        public HkjcFootballRepository(HkjcFootballDbContext hkjcFootballDbContext)
        {
            this._context = hkjcFootballDbContext;
        }


        public async Task<IEnumerable<HkjcDataPoolResult>> GetResults()
        {
           
            return await _context.HkjcDataPoolResults.Take(100).ToListAsync();

        }




        public async Task<bool> ImportHkjcFootballRecordsWsByString (string csvObjs)
        {
            bool result = false;

            if (csvObjs.Length > 0)
            {
                

                try
                {
                    List<CustomHkjcFootballObj> srcs = PrepareFootballRecords(csvObjs);

                    DateTime minMatchDt = (from p in srcs select p.Matchdt).Min().Value;


                    List<string> KeysInDb = await _context.HkjcDataPools.Where(x => x.Matchdt >= minMatchDt).Select(p => p.Weekday.Trim() + p.Matchtype.Trim() + p.Matchname.Trim() + p.Matchdate.Trim()).ToListAsync();

                    List<CustomHkjcFootballObj> tempScrs = (from p in srcs
                                                            where !KeysInDb.Contains(p.Weekday.Trim() + p.Matchtype.Trim() + p.Matchname.Trim() + p.Matchdate.Trim())
                                                            && (DecimalExtension.Convert(p.Fh) > 0 && DecimalExtension.Convert(p.Fd) > 0 && DecimalExtension.Convert(p.Fa) > 0)
                                                            && (DecimalExtension.Convert(p.H) > 0 && DecimalExtension.Convert(p.D) > 0 && DecimalExtension.Convert(p.A) > 0)
                                                            select p).ToList();


                    List<HkjcDataPoolResult> tempSrcs_result = (from p in tempScrs
                                                                select new HkjcDataPoolResult
                                                                {
                                                                    Weekday = p.Weekday,
                                                                    Matchname = p.Matchname,
                                                                    Matchdt = p.Matchdt.Value,
                                                                    Hr1 = string.Empty,
                                                                    Hr2 = string.Empty,
                                                                    Fr1 = string.Empty,
                                                                    Fr2 = string.Empty,
                                                                    FrHad = string.Empty,
                                                                    Createdon = DateTimeExtension.GetChinaStandardDatetime()
                                                                }).ToList();

                    List<CustomHkjcFootballObj> exist_tempScrs = (from p in srcs
                                                                  where KeysInDb.Contains(p.Weekday.Trim() + p.Matchtype.Trim() + p.Matchname.Trim() + p.Matchdate.Trim())
                                                                  && (DecimalExtension.Convert(p.Fh) > 0 && DecimalExtension.Convert(p.Fd) > 0 && DecimalExtension.Convert(p.Fa) > 0)
                                                                  && (DecimalExtension.Convert(p.H) > 0 && DecimalExtension.Convert(p.D) > 0 && DecimalExtension.Convert(p.A) > 0)
                                                                  select p).ToList();

                    foreach (string keyInDb in KeysInDb)
                    {
                        HkjcDataPool x_dbObj = await _context.HkjcDataPools.Where(p => (p.Weekday.Trim() + p.Matchtype.Trim() + p.Matchname.Trim() + p.Matchdate.Trim()).Equals(keyInDb)).FirstOrDefaultAsync();

                        CustomHkjcFootballObj exist_tempScr = exist_tempScrs.Where(p => p.CombintedKey.Equals(keyInDb)).FirstOrDefault();

                        if (x_dbObj != null && exist_tempScr != null)
                        {
                            decimal noOfMinutes = DateTimeExtension.GetNoOfMinutes(DateTimeExtension.GetChinaStandardDatetime(), x_dbObj.Matchdt.Value);

                            if (noOfMinutes >= 28)
                            {
                                x_dbObj.FhUp = exist_tempScr.Fh;
                                x_dbObj.FdUp = exist_tempScr.Fd;
                                x_dbObj.FaUp = exist_tempScr.Fa;
                                x_dbObj.HUp = exist_tempScr.H;
                                x_dbObj.DUp = exist_tempScr.D;
                                x_dbObj.AUp = exist_tempScr.A;
                                x_dbObj.MatchdtUp = DateTimeExtension.GetChinaStandardDatetime();
                            }
                        }

                    }

                    await _context.HkjcDataPools.AddRangeAsync(ConvertHkjcFootballObj(tempScrs));
                    await _context.HkjcDataPoolResults.AddRangeAsync(tempSrcs_result);
                    await _context.SaveChangesAsync();

                    //LoggingManager.Log(new logging { Message = "HkjcManager > Successfully: ImportRecords! @" + DateTimeExtension.GetChinaStandardDatetime().ToString() });

                    result = true;
                }
                catch (Exception ex)
                {
                    //LoggingManager.Log(new logging { Message = "HkjcManager > Failure: ImportHkjcFootballRecords! @" + DateTimeExtension.GetChinaStandardDatetime().ToString(), Event = "ImportHkjcFootballRecords", Pages = "HkjcManager.cs", SeqNo = "0" });
                    //LoggingManager.Log(new logging { Message = "HkjcManager > Failure: ImportHkjcFootballRecords! @" + ex.Message + " " + DateTimeExtension.GetChinaStandardDatetime().ToString(), Event = "ImportHkjcFootballRecords", Pages = "HkjcManager.cs", SeqNo = "1" });
                    //throw ex;
                }
            }

            return result;
        }

        private List<CustomHkjcFootballObj> PrepareFootballRecords(string csvObjs)
        {
            List<CustomHkjcFootballObj> records = new List<CustomHkjcFootballObj>();
            string temp_csvObjs = csvObjs.Replace("{", "").Replace("}", "");
            List<string> theObjs = temp_csvObjs.Split("|".ToArray()).ToList();

            foreach (string theObj in theObjs)
            {

                records.Add(new CustomHkjcFootballObj
                {
                    Url = GetCustomHkjcFootballObjFieldValue(theObj, "url"),
                    Matchid = GetCustomHkjcFootballObjFieldValue(theObj, "matchid"),
                    Weekday = GetCustomHkjcFootballObjFieldValue(theObj, "weekday"),
                    Matchtype = GetCustomHkjcFootballObjFieldValue(theObj, "matchtype"),
                    Matchdate = GetCustomHkjcFootballObjFieldValue(theObj, "matchdate"),
                    Matchname = GetCustomHkjcFootballObjFieldValue(theObj, "matchname"),
                    Fh = GetCustomHkjcFootballObjFieldValue(theObj, "fh"),
                    Fd = GetCustomHkjcFootballObjFieldValue(theObj, "fd"),
                    Fa = GetCustomHkjcFootballObjFieldValue(theObj, "fa"),
                    H = GetCustomHkjcFootballObjFieldValue(theObj, "h"),
                    D = GetCustomHkjcFootballObjFieldValue(theObj, "d"),
                    A = GetCustomHkjcFootballObjFieldValue(theObj, "a"),
                    Line = GetCustomHkjcFootballObjFieldValue(theObj, "line"),
                    Big = GetCustomHkjcFootballObjFieldValue(theObj, "big"),
                    Small = GetCustomHkjcFootballObjFieldValue(theObj, "small"),
                    Hline = GetCustomHkjcFootballObjFieldValue(theObj, "hline"),
                    Hbig = GetCustomHkjcFootballObjFieldValue(theObj, "hbig"),
                    Hsmall = GetCustomHkjcFootballObjFieldValue(theObj, "hsmall"),
                    T0 = GetCustomHkjcFootballObjFieldValue(theObj, "T0"),
                    T1 = GetCustomHkjcFootballObjFieldValue(theObj, "T1"),
                    T2 = GetCustomHkjcFootballObjFieldValue(theObj, "T2"),
                    T3 = GetCustomHkjcFootballObjFieldValue(theObj, "T3"),
                    T4 = GetCustomHkjcFootballObjFieldValue(theObj, "T4"),
                    T5 = GetCustomHkjcFootballObjFieldValue(theObj, "T5"),
                    T6 = GetCustomHkjcFootballObjFieldValue(theObj, "T6"),
                    T7Plus = GetCustomHkjcFootballObjFieldValue(theObj, "T7_plus"),
                    Odd = GetCustomHkjcFootballObjFieldValue(theObj, "odd"),
                    Even = GetCustomHkjcFootballObjFieldValue(theObj, "even"),
                    Hh = GetCustomHkjcFootballObjFieldValue(theObj, "HH"),
                    Hd = GetCustomHkjcFootballObjFieldValue(theObj, "HD"),
                    Ha = GetCustomHkjcFootballObjFieldValue(theObj, "HA"),
                    Dh = GetCustomHkjcFootballObjFieldValue(theObj, "DH"),
                    Dd = GetCustomHkjcFootballObjFieldValue(theObj, "DD"),
                    Da = GetCustomHkjcFootballObjFieldValue(theObj, "DA"),
                    Ah = GetCustomHkjcFootballObjFieldValue(theObj, "AH"),
                    Ad = GetCustomHkjcFootballObjFieldValue(theObj, "AD"),
                    Aa = GetCustomHkjcFootballObjFieldValue(theObj, "AA"),
                    Fr10 = GetCustomHkjcFootballObjFieldValue(theObj, "FR10"),
                    Fr20 = GetCustomHkjcFootballObjFieldValue(theObj, "FR20"),
                    Fr21 = GetCustomHkjcFootballObjFieldValue(theObj, "FR21"),
                    Fr30 = GetCustomHkjcFootballObjFieldValue(theObj, "FR30"),
                    Fr31 = GetCustomHkjcFootballObjFieldValue(theObj, "FR31"),
                    Fr32 = GetCustomHkjcFootballObjFieldValue(theObj, "FR32"),
                    Fr00 = GetCustomHkjcFootballObjFieldValue(theObj, "FR00"),
                    Fr11 = GetCustomHkjcFootballObjFieldValue(theObj, "FR11"),
                    Fr22 = GetCustomHkjcFootballObjFieldValue(theObj, "FR22"),
                    Fr33 = GetCustomHkjcFootballObjFieldValue(theObj, "FR33"),
                    Fr01 = GetCustomHkjcFootballObjFieldValue(theObj, "FR01"),
                    Fr02 = GetCustomHkjcFootballObjFieldValue(theObj, "FR02"),
                    Fr12 = GetCustomHkjcFootballObjFieldValue(theObj, "FR12"),
                    Fr03 = GetCustomHkjcFootballObjFieldValue(theObj, "FR03"),
                    Fr13 = GetCustomHkjcFootballObjFieldValue(theObj, "FR13"),
                    Fr23 = GetCustomHkjcFootballObjFieldValue(theObj, "FR23"),
                    Hr10 = GetCustomHkjcFootballObjFieldValue(theObj, "HR10"),
                    Hr20 = GetCustomHkjcFootballObjFieldValue(theObj, "HR20"),
                    Hr21 = GetCustomHkjcFootballObjFieldValue(theObj, "HR21"),
                    Hr30 = GetCustomHkjcFootballObjFieldValue(theObj, "HR30"),
                    Hr31 = GetCustomHkjcFootballObjFieldValue(theObj, "HR31"),
                    Hr32 = GetCustomHkjcFootballObjFieldValue(theObj, "HR32"),
                    Hr00 = GetCustomHkjcFootballObjFieldValue(theObj, "HR00"),
                    Hr11 = GetCustomHkjcFootballObjFieldValue(theObj, "HR11"),
                    Hr22 = GetCustomHkjcFootballObjFieldValue(theObj, "HR22"),
                    Hr33 = GetCustomHkjcFootballObjFieldValue(theObj, "HR33"),
                    Hr01 = GetCustomHkjcFootballObjFieldValue(theObj, "HR01"),
                    Hr02 = GetCustomHkjcFootballObjFieldValue(theObj, "HR02"),
                    Hr12 = GetCustomHkjcFootballObjFieldValue(theObj, "HR12"),
                    Hr03 = GetCustomHkjcFootballObjFieldValue(theObj, "HR03"),
                    Hr13 = GetCustomHkjcFootballObjFieldValue(theObj, "HR13"),
                    Hr23 = GetCustomHkjcFootballObjFieldValue(theObj, "HR23"),
                    HhaHg = GetCustomHkjcFootballObjFieldValue(theObj, "HHA_HG"),
                    HhaH = GetCustomHkjcFootballObjFieldValue(theObj, "HHA_H"),
                    HhaD = GetCustomHkjcFootballObjFieldValue(theObj, "HHA_D"),
                    HhaA = GetCustomHkjcFootballObjFieldValue(theObj, "HHA_A"),
                    HhaAg = GetCustomHkjcFootballObjFieldValue(theObj, "HHA_AG"),
                    FtsH = GetCustomHkjcFootballObjFieldValue(theObj, "FTS_H"),
                    FtsN = GetCustomHkjcFootballObjFieldValue(theObj, "FTS_N"),
                    FtsA = GetCustomHkjcFootballObjFieldValue(theObj, "FTS_A"),
                    Hr1 = GetCustomHkjcFootballObjFieldValue(theObj, "HR1"),
                    Hr2 = GetCustomHkjcFootballObjFieldValue(theObj, "HR2"),
                    Fr1 = GetCustomHkjcFootballObjFieldValue(theObj, "FR1"),
                    Fr2 = GetCustomHkjcFootballObjFieldValue(theObj, "FR2"),
                    FrHad = GetCustomHkjcFootballObjFieldValue(theObj, "FR_HAD")
                });
            }

            return records;
        }

        private string GetCustomHkjcFootballObjFieldValue(string objPropertyText, string fieldName)
        {
            string result = string.Empty;
            int pointerStart = 0;
            int pointerEnd = 0;

            //  'fieldName': '
            string theKey = "'" + fieldName + "': '";
            pointerStart = objPropertyText.IndexOf(theKey, 0) + theKey.Length;
            pointerEnd = objPropertyText.IndexOf("'", pointerStart);

            if (pointerStart > 0 && pointerEnd > 0)
            {
                result = objPropertyText.Substring(pointerStart, pointerEnd - pointerStart).Trim();
            }

            return result;

        }

        private List<HkjcDataPool> ConvertHkjcFootballObj(List<CustomHkjcFootballObj> objs)
        {
            List<HkjcDataPool> resultList = new List<HkjcDataPool>();
            DateTime now = DateTimeExtension.GetChinaStandardDatetime();

            foreach (CustomHkjcFootballObj obj in objs)
            {
                HkjcDataPool result = new HkjcDataPool();

                result.Weekday = obj.Weekday;
                result.Matchtype = obj.Matchtype;
                result.Matchdate = obj.Matchdate;
                result.Matchname = obj.Matchname;
                result.Matchdt = obj.Matchdt;
                result.Fh = obj.Fh;
                result.Fd = obj.Fd;
                result.Fa = obj.Fa;
                result.H = obj.H;
                result.D = obj.D;
                result.A = obj.A;
                result.FhUp = obj.Fh;
                result.FdUp = obj.Fd;
                result.FaUp = obj.Fa;
                result.HUp = obj.H;
                result.DUp = obj.D;
                result.AUp = obj.A;
                result.MatchdtUp = now;
                result.Line = obj.Line;
                result.Big = obj.Big;
                result.Small = obj.Small;
                result.Hline = obj.Hline;
                result.Hbig = obj.Hbig;
                result.Hsmall = obj.Hsmall;
                result.T0 = obj.T0;
                result.T1 = obj.T1;
                result.T2 = obj.T2;
                result.T3 = obj.T3;
                result.T4 = obj.T4;
                result.T5 = obj.T5;
                result.T6 = obj.T6;
                result.T7Plus = obj.T7Plus;
                result.Odd = obj.Odd;
                result.Even = obj.Even;
                result.Hh = obj.Hh;
                result.Hd = obj.Hd;
                result.Ha = obj.Ha;
                result.Dh = obj.Dh;
                result.Dd = obj.Dd;
                result.Da = obj.Da;
                result.Ah = obj.Ah;
                result.Ad = obj.Ad;
                result.Aa = obj.Aa;
                result.Fr10 = obj.Fr10;
                result.Fr20 = obj.Fr20;
                result.Fr21 = obj.Fr21;
                result.Fr30 = obj.Fr30;
                result.Fr31 = obj.Fr31;
                result.Fr32 = obj.Fr32;
                result.Fr00 = obj.Fr00;
                result.Fr11 = obj.Fr11;
                result.Fr22 = obj.Fr22;
                result.Fr33 = obj.Fr33;
                result.Fr01 = obj.Fr01;
                result.Fr02 = obj.Fr02;
                result.Fr12 = obj.Fr12;
                result.Fr03 = obj.Fr03;
                result.Fr13 = obj.Fr13;
                result.Fr23 = obj.Fr23;
                result.Hr10 = obj.Hr10;
                result.Hr20 = obj.Hr20;
                result.Hr21 = obj.Hr21;
                result.Hr30 = obj.Hr30;
                result.Hr31 = obj.Hr31;
                result.Hr32 = obj.Hr32;
                result.Hr00 = obj.Hr00;
                result.Hr11 = obj.Hr11;
                result.Hr22 = obj.Hr22;
                result.Hr33 = obj.Hr33;
                result.Hr01 = obj.Hr01;
                result.Hr02 = obj.Hr02;
                result.Hr12 = obj.Hr12;
                result.Hr03 = obj.Hr03;
                result.Hr13 = obj.Hr13;
                result.Hr23 = obj.Hr23;
                result.HhaHg = obj.HhaHg;
                result.HhaH = obj.HhaH;
                result.HhaD = obj.HhaD;
                result.HhaA = obj.HhaA;
                result.HhaAg = obj.HhaAg;
                result.FtsH = obj.FtsH;
                result.FtsN = obj.FtsN;
                result.FtsA = obj.FtsA;
                result.Hr1 = string.Empty;
                result.Hr2 = string.Empty;
                result.Fr1 = string.Empty;
                result.Fr2 = string.Empty;
                result.FrHad = string.Empty;
                result.Createdon = now;
                result.Createdby = "SYSTEM";

                resultList.Add(result);

            }

            return resultList;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }
        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ProductRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
