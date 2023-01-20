using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    public class TalukaData
    {
        string _TalukaName ;
        int _TalukaID;
        long _TalukaEntryCount;

        public string TalukaName { get { return _TalukaName; } }
        public int TalukaID { get { return _TalukaID; } }
        public long TalukaEntryCount { get { return _TalukaEntryCount; } }

        public TalukaData(int TalukaID, string TalukaName, long TalukaEntryCount)
            {
                _TalukaName = TalukaName;
                _TalukaID = TalukaID;
                _TalukaEntryCount = TalukaEntryCount;
            }
    }
