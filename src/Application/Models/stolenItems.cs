namespace Application.Models
{
    using System;

    public class stolenItem
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public DateTime DateStolen { get; set; }
        public string LocationStolen { get; set; }
        public bool IsRecovered { get; set; }
        public DateTime? DateRecovered { get; set; }
        public string AdditionalNotes { get; set; }

        public stolenItem(
            string itemName,
            string description,
            DateTime dateStolen,
            string locationStolen,
            string policeReportNumber,
            bool isRecovered,
            DateTime? dateRecovered,
            string additionalNotes)
        {
            ItemName = itemName;
            Description = description;
            DateStolen = dateStolen;
            LocationStolen = locationStolen;
            IsRecovered = isRecovered;
            DateRecovered = dateRecovered;
            AdditionalNotes = additionalNotes;
        }
    }

}



