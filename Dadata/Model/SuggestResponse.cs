using System;
using System.Collections.Generic;

namespace Dadata.Model
{
    public class SuggestResponse<T>
    {
        public IList<Suggestion<T>> suggestions;
    }
}
