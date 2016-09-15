namespace WarmBeer.Core.Domain
{
    public class Phone
    {
        public string Areacode { get; set; }

        public string Number { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}", this.Areacode, this.Number);
        }
    }
}
