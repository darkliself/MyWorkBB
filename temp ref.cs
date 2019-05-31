


R("SP-22479").HasValue() ? R("SP-22479").Replace("<NULL>", "").Text :
		R("cnet_common_SP-22479").HasValue() ? R("cnet_common_SP-22479").Replace("<NULL>", "").Text : "";

        @"(?<=d) (?=(yd(s)|mm|cm|m|L|mL|gram|cup(s)|qt|MB/s)( |$||))",
        @"(?<=\d) (?=(yd\(s\)|mm|cm|m|L|mL|gram|cup\(s\)|qt|MB\/s)( |$|\|))",



REQ.GetVariable("SP-22479").HasValue() ? REQ.GetVariable("SP-22479") : R("SP-22479").HasValue() ? R("SP-22479") : R("cnet_common_SP-22479");


Add(Coalesce(test).RegexReplace("([A-Z]{2,})|([A-Z][a-z])", "$1".ToLower()));