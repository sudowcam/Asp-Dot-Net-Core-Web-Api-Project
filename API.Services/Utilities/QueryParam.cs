namespace Todo.API.Utilities
{
    /// <summary>
    ///   list of query parameters available for filtering, sorting and searching. 
    /// </summary>
    public class QueryParameters
    {
        /// <summary>
        ///   General overall search
        /// </summary>
        public string? GeneralSearch { get; set; }

        /// <summary>
        ///   Specific title filter
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        ///   Specific ownership filter
        /// </summary>
        public string? Ownership { get; set; }
        /// <summary>
        ///   Specific status filter
        /// </summary>
        public string? Status { get; set; }
        ///   Specific order based on which element header and what order
        ///   
        ///   Owner asc
        ///   Owner,Status desc
        /// </summary>
        public string OrderBy { get; set; }

        public QueryParameters()
        {
            OrderBy = "name";
        }


    }
}
