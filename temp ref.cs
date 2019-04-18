!(R("SP-24688") is null) || !R("SP-24688").Text.Equals("<NULL>") ? R("SP-24688").Text : 
    !(R("cnet_common_SP-24688") is null) || !R("cnet_common_SP-24688").Text.Equals("<NULL>") ? R("cnet_common_SP-24688").Text : "";



!(R("SP-22288") is null) || !R("SP-22288").Text.Equals("<NULL>") ? Coalesce(R("SP-22288").Text) : 
    !(R("cnet_common_SP-22288") is null) || !R("cnet_common_SP-22288").Text.Equals("<NULL>") ? Coalesce(R("cnet_common_SP-22288").Text) : Coalesce("");

R("SP-18656").HasValue() ? R("SP-18656").Text : R("cnet_common_SP-18656").HasValue() ? R("cnet_common_SP-18656").Text : "";

R("SP-18656").HasValue() ? R("SP-18656").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18656").HasValue() ? R("cnet_common_SP-18656").Replace("<NULL>", "").Text : "";

        @"(?<=d) (?=(yd(s)|mm|cm|m|L|mL|gram|cup(s)|qt|MB/s)( |$||))",
        @"(?<=\d) (?=(yd\(s\)|mm|cm|m|L|mL|gram|cup\(s\)|qt|MB\/s)( |$|\|))",


Add(Coalesce(test).RegexReplace("([A-Z]{2,})|([A-Z][a-z])", "$1".ToLower()));