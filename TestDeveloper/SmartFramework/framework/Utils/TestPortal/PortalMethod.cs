namespace smart.framework.Utils.TestPortal
{
    public class PortalMethod
    {
        private PortalMethod(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public static PortalMethod GetToken => new PortalMethod("/token/get");

        public static PortalMethod GetXmlTestsList => new PortalMethod("/test/get/xml");

        public override string ToString() => Value;
    }
}
