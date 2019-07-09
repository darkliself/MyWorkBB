/* 
--width(354) -> lenght, depth(355) - width, height(356)
--size (6486) - (W) cm x (D) cm x (H) cm

    Envelopes & Shipping Boxes (1A) -- 6486
    0E (Filing & Storage) -- 5924
    1C (Packaging Materials) -- 6521

*/

var length = Coalesce(A[354], A[6486], A[6521], A[5924]);

if (length.HasValue() && length.Values.Flatten(" ").In("% cm%", "% mm%", "% m%")) {
    complexResult(length.Values.Flatten(" ").Replace(",", "."));
} else if (length.HasValue() && length.Values.ExtractNumbers().Any()) {
    var val = length.Units;
    if (val.Where(s => s.NameUSM.In("in")).Any()) {
        Add(length.FirstValueUsm());
    } else if(val.Where(s => s.Name.In("cm", "mm", "m")).Any()){
        metricResult(val.First().Name, length.FirstValue());
    }
} 

// for 355 attr
void metricResult(string valUnits, double val) {
    double result = val * 0.0393701;
    if (valUnits.Equals("mm")) {
        result = Math.Round(result, 2);
    } else if (valUnits.Equals("cm")) {
        result = Math.Round(result * 10, 2);
    } else if (valUnits.Equals("m")) {
        result = Math.Round(result * 1000, 2);
    }
    if (result > 0) {
        showResult(result);
    }
}

// for other attr 6486, 5924, 6521
void complexResult(string complexVal) {
    var firstVal = Coalesce(complexVal);
    var tmp =  firstVal.ExtractNumbers();
    double result = 0;
    if (tmp.Count > 1 && tmp[1].HasValue()) {
         result = (double)tmp[1] * 0.0393701;
    }
    if (complexVal.Split(" x ").Count() > 1 && complexVal.Split(" x ")[1].Contains("cm")) {
        result = Math.Round((result * 10), 2);
    } else if (complexVal.Split(" x ").Count() > 1 && complexVal.Split(" x ")[1].Contains("mm")) {
        result = Math.Round(result, 2);
    } else if (complexVal.Split(" x ").Count() > 1 && complexVal.Split(" x ")[1].Contains("m")) {
         result = Math.Round((result * 1000), 2);
    }
    if (result > 0) {
       showResult(result);
    }
}

// show and format result
void showResult(double num) {
    if (num > 0) {
        var formattedResult = Coalesce(num);
        if (formattedResult.In("%.52", "%.51", "%.74", "%.76","%.49")) {
            formattedResult = formattedResult
                .Replace(".52", ".5", ".51", ".5", ".74", ".75", ".76", ".75", ".49", ".5");
            Add(formattedResult);
        }
        else if (formattedResult.In( "%.9%", "%.0%", "%.8%")) {
            Add(Math.Round(decimal.Parse(formattedResult), 0));
        } else {
            Add(num);
        }
    }
}