using GraphQL.Types;

namespace GraphQLNetExample.Note
{
    public class NotesSchema : Schema
    {
        public NotesSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<NotesQuery>();
        }

    }
}
