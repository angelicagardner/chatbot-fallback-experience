public class ParagraphInputType : InputObjectGraphType<Paragraph>
{
    public ParagraphInputType()
    {
        Name = "ParagraphInput";
        Field(p => p.Text);
    }
}