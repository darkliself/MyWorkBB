/*
IF A[5965].Where("compartment").Match(5971).Values IS NOT NULL --- Compartments Qty
AND A[5965].Where("compartment").Match(5969).ValuesUSM IS NOT NULL -- Compartment Height
AND A[5965].Where("compartment").Match(5969).ValuesAndUnitsUSM.FlattenWithAnd() LIKE "%in%" 
AND A[5922].Value LIKE "storage clipboard" 
THEN "Clipboard has "_A[5965].Where("compartment").Match(5971).Values.Flatten().IfLike("2", "dual").IfLike("3", "triple ")_" "_A[5965].Where("compartment").Match(5969).ValuesUSM_""" capacity storage compartment" 

ELSE IF A[5965].Where("compartment").Match(5969).ValuesUSM IS NOT NULL -- Compartment Height
AND A[5965].Where("compartment").Match(5969).ValuesAndUnitsUSM.FlattenWithAnd() LIKE "%in%" 
AND A[5922].Value LIKE "storage clipboard" 
THEN "Clipboard has "_A[5965].Where("compartment").Match(5969).ValuesUSM.FlattenWithAnd()_""" capacity storage compartment";

*/

Some();
void Some() {
    if (A[5965].HasValue() && A[5965].Where("compartment").Match(5971).HasValue() && A[5965].HasValue() && A[5965].Where("compartment").Match(5969).HasValue()) {
       Add("WOrk");
        Add(A[5965].Where("compartment").Match(5971).Values("x"));
        Add(A[5965].Where("compartment").Match(5969).Values("x"));
        
    }
}
NutritionalInformation();
void NutritionalInformation() {
    var repeatingSets = new []{A[5965], A[5971], A[5969]};
    var separators = new []{" ", "()x"};
    var d = new Dictionary<int, string>();
    int i = 0;
    int j = -1;
    string v="";
    
    foreach (var attr in repeatingSets){
        if (attr.HasValue()){
            foreach (var attrValue in attr.GetValuesWithUnits()){
                i = attrValue.Value.SetNo;
                //Add(attrValue.Value.Value() + " here");
                v = (attrValue.Value.ValueUSM==""?attrValue.Value.Value():attrValue.Value.ValueUSM)+" "+(attrValue.Unit.NameUSM!=""?attrValue.Unit.NameUSM:attrValue.Unit.Name);
                if(d.ContainsKey(i))
                {
                    d[i]=d[i]+(j>=0?separators[j]:"")+ v;
                    //Add(v);
                }
                else
                {
                    d.Add(i, v);
                }
            }
        }
        j=j+1;
    }
    var some = "";
    Add(d.Values.Flatten());
    Add(d.Values.Flatten().HasValue("% mm"));
    if (d.Values.Flatten().HasValue("% mm")) {
        some = d.Values.Where(
            o => (Coalesce(o).ExtractNumbers().Any() && Coalesce(o).ExtractNumbers().First() > 0)
                || !Coalesce(o).ExtractNumbers().Any()
                ).FlattenWithAnd().ExtractNumbers().Last();
        foreach (var item in d.Values) {
            if (Coalesce(item).ExtractNumbers().Any() && Coalesce(item).ExtractNumbers().Count() == 1) {
                Add($"Clipboard has {Math.Round(Coalesce(item).ExtractNumbers().First() * 0.0393701, 2)}\" capacity storage compartment");
                
            }
            else if (Coalesce(item).ExtractNumbers().Any() && Coalesce(item).ExtractNumbers().Count() == 2) {
                Add($"Clipboard has {Math.Round(Coalesce(item).ExtractNumbers().First() * 0.0393701, 2)}\" capacity storage compartment");
            }
            
        }
    }
    
    if (some != "") {
        Add($"NutritionalInformation⸮Each ##K-Cup contains {some}");
    }
}

// --[FEATURE #12]
void AdditionalMixedTea() {
    if (SKU.ProductId.In("20658244", "20658237"))
    {
        Add("AdditionalMixedTea⸮Blend of decaffeinated green tea and decaffeinated ##Bai ##Mu ##Dan white tea");
    }
}