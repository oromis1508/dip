namespace VkApi.Forms
{
    internal class NavigateMenuItem
    {
        private NavigateMenuItem(string value) => Value = value;

        private string Value { get; }

        public static NavigateMenuItem MyPage => new NavigateMenuItem("l_pr");

        public static NavigateMenuItem News => new NavigateMenuItem("l_nwsf");

        public static NavigateMenuItem Messages => new NavigateMenuItem("l_msg");

        public static NavigateMenuItem Friends => new NavigateMenuItem("l_fr");

        public static NavigateMenuItem Groups => new NavigateMenuItem("l_gr");

        public static NavigateMenuItem Photos => new NavigateMenuItem("l_ph");

        public static NavigateMenuItem Music => new NavigateMenuItem("l_aud");

        public static NavigateMenuItem Videos => new NavigateMenuItem("l_vid");

        public static NavigateMenuItem Games => new NavigateMenuItem("l_ap");

        public static NavigateMenuItem Goods => new NavigateMenuItem("l_mk");

        public static NavigateMenuItem Documents => new NavigateMenuItem("l_doc");

        public static NavigateMenuItem Management => new NavigateMenuItem("l_apm");

        public override string ToString() => Value;
    }
}
