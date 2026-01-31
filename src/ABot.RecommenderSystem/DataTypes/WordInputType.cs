public class WordInputType : InputObjectGraphType<Word>
{
    public WordInputType()
    {
        Name = "WordInput";
        Field(w => w.Text);
    }
}