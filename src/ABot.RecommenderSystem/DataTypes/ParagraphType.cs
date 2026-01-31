public class ParagraphType : ObjectGraphType<Paragraph>
{
    public ParagraphType(RecommenderSystemData data)
    {
        Name = "Paragraph";
        Field(p => p.Id).Description("The unique identifier of the paragraph.");
        Field(p => p.Text).Description("The raw text content of the paragraph.");

        Field<ListGraphType<WordType>>(
            "words",
            resolve: context => data.GetWordsForParagraph(context.Source.Id)
        );
    }
}