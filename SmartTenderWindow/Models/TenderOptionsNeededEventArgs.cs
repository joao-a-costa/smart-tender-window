using System;
using System.Collections.Generic;

namespace SmartTenderWindowTenderSplit.Models
{
    public class TenderOptionsNeededEventArgs : EventArgs
    {
        public TenderItem Tender { get; }
        public TenderTypeEnum? TenderType { get; }
        public int TenderIndex { get; }

        // Caller sets whichever lists are relevant for the TenderType
        public List<TenderOption> BeneficiaryAccounts { get; set; }
        public List<TenderOption> PartyAccounts { get; set; }
        public List<TenderOption> Banks { get; set; }
        public List<TenderOption> Series { get; set; }

        internal TenderOptionsNeededEventArgs(TenderItem tender, int index)
        {
            Tender      = tender;
            TenderType  = tender.TenderType;
            TenderIndex = index;
        }
    }
}
