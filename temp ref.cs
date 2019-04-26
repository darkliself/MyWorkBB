


R("SP-382").HasValue() ? R("SP-382").Text : R("cnet_common_SP-382").HasValue() ? R("cnet_common_SP-382").Text : "";

R("SP-382").HasValue() ? R("SP-382").Replace("<NULL>", "").Text :
		R("cnet_common_SP-382").HasValue() ? R("cnet_common_SP-382").Replace("<NULL>", "").Text : "";

        @"(?<=d) (?=(yd(s)|mm|cm|m|L|mL|gram|cup(s)|qt|MB/s)( |$||))",
        @"(?<=\d) (?=(yd\(s\)|mm|cm|m|L|mL|gram|cup\(s\)|qt|MB\/s)( |$|\|))",


Add(Coalesce(test).RegexReplace("([A-Z]{2,})|([A-Z][a-z])", "$1".ToLower()));