namespace API.Services.Test
{
    internal class RequestNoteProcessor
    {
        public RequestNoteProcessor()
        {
        }

        internal ResponseNote GetNoteList(RequestNoteList request)
        {
            return new ResponseNote();
        }

        internal ResponseNote GetNoteDetails(RequestNoteDetails request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new ResponseNote
            {
                NoteId = request.NoteId
            };
        }
    }
}