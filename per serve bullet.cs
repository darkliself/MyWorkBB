
// Working SKU 20647837

NutritionalInformation();
void NutritionalInformation() {
    if (A[11359].HasValue("%serving") && A[11248].HasValue() && A[11249].HasValue()) {
        if (A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => o.In("%x <NULL>")).Flatten("|").HasValue()
        && A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => !o.In("%x <NULL>", "%x 0")).Flatten("|").HasValue()) {
            var units = A[11249].GetValuesWithUnits().Select(o => o.Value.Value() + " x " + o.Unit.Name).Where(s => !s.Contains("0 x")).Flatten("|").Replace(" x", ""," g", " grams", " mg", "mg", "1 grams", "1 gram").ToString().Split("|");
            var ingradiets = A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => !o.In("%x 0", "%x <NULL>")).Flatten("|").RegexReplace("( x [0-9]+)", "").ToString().Split("|");
            if (units.Length > 0 && (units.Length == ingradiets.Length)) {
                var index = 0;
                var endIndex = units.Length;
                var arr = new List<string>();
                foreach (var item in units) {
                    if (index + 1 == endIndex) {
                        arr.Add($"and {ingradiets[index]} ({units[index]})");
                    }
                    else {
                        arr.Add($"{ingradiets[index]} ({units[index]})");
                    }
                    index++;
                }
                foreach(var item in arr) {
                    Add(item);
                }
                Add($"NutritionalInformation⸮Each ##K-Cup includes {A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => o.In("%x <NULL>")).Flatten(", ").Replace(" x <NULL>", "").RegexReplace(" ([A-Z])", " ##$1")} {Coalesce(String.Join(", ", arr)).RegexReplace(" ([A-Z])", " ##$1")}" );
            }
            else {
                 Add($"NutritionalInformation⸮Each ##K-Cup includes {A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => o.In("%x <NULL>")).Flatten(", ").Replace(" x <NULL>", "").RegexReplace(" ([A-Z])", " ##$1")}");
            }
            
            Add($"NutritionalInformation⸮Each ##K-Cup includes {A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => o.In("%x <NULL>")).Flatten("|")}" 
            + $"{A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => !o.In("%x <NULL>", "%x 0")).Flatten("|")}");
        }
        else if (A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => o.In("%x <NULL>")).Flatten("|").HasValue()) {
             Add($"NutritionalInformation⸮Each ##K-Cup includes {A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => o.In("%x <NULL>")).FlattenWithAnd().Replace(" x <NULL>", "").RegexReplace(" ([A-Z])", " ##$1")}");
        }
        else if (A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => !o.In("%x <NULL>", "%x 0")).Flatten("|").HasValue()) {
            var units = A[11249].GetValuesWithUnits().Select(o => o.Value.Value() + " x " + o.Unit.Name).Where(s => !s.Contains("0 x")).Flatten("|").Replace(" x", ""," g", " grams", " mg", "mg", "1 grams", "1 gram").ToString().Split("|");
            var ingradiets = A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => !o.In("%x 0", "%x <NULL>")).Flatten("|").RegexReplace("( x [0-9]+)", "").ToString().Split("|");
            if (units.Length > 0 && (units.Length == ingradiets.Length)) {
                var index = 0;
                var endIndex = units.Length;
                var arr = new List<string>();
                foreach (var item in units) {
                    if (index + 1 == endIndex) {
                        arr.Add($"and {ingradiets[index]} ({units[index]})");
                    }
                    else {
                        arr.Add($"{ingradiets[index]} ({units[index]})");
                    }
                    index++;
                }
                Add($"NutritionalInformation⸮Each ##K-Cup includes {Coalesce(String.Join(", ", arr)).RegexReplace(" ([A-Z])", " ##$1")}" );
            }
             
        }
    }
}

Add(A[11249].Units.Select(o => o.Name));


Add(A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => !o.In("%x <NULL>", "%x 0")).Flatten("|").HasValue());
Add(A[11248].Values.Match(11249, 11248).Values(" x ").Where(o => o.In("%x <NULL>")).Flatten("|").HasValue());

Add(A[11249].HasValue("<NULL>"));


var a = A[11248].Values.Match(11248, 11249).Values(" x ").Where(s => s.NotIn("% x 0%"));

if(a.Any()){
    var b = a.Select(
        s => A[11249].HasValue(s.Text.Split(" x ").Last()) ? 
        $"{s.Text.Split(" x ").First()} ({s.Text.Split(" x ").Last()} {A[11249].Units.First().Name})".Flatten("").Replace(" g)", " grams)") : 
        s.Replace(" x <NULL>", ""));
    Add($"Each ##K-Cup includes {b.FlattenWithAnd()}");
}