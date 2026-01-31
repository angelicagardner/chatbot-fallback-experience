public class WordType : ObjectGraphType<Word>
{
    public WordType()
    {
        Name = "Word";
        Field(w => w.Id).Description("The unique identifier of the word node.");
        Field(w => w.Text).Description("The actual string value of the word.");
    }
}