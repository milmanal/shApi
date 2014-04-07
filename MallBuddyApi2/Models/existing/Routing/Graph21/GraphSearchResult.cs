using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MallBuddyApi2.Models.existing.Routing.Graph21
{

    public class GraphSearchResult : IEnumerable<GraphDataItem>
    {
        private List<GraphDataItem> mList = new List<GraphDataItem>();

        public GraphSearchResult(Vertex vertex, TimeSpan time, int tries)
        {
            while (vertex != null && vertex.PreviousEdge != null)
            {
                var dataItem = vertex.PreviousEdge.DataItem.Clone();
                dataItem.IsReverse = vertex.PreviousEdge.IsReverse;
                mList.Insert(0, dataItem);
                vertex = vertex.PreviousVertex;
            }
            var i = 1;
            foreach (var item in mList)
            {
                item.Num = i;
                i++;
            }
            PathFound = vertex != null;
            TimeSpent = time;
            Tries = tries;
        }

        public bool PathFound { get; private set; }
        public TimeSpan TimeSpent { get; private set; }
        public int Tries { get; private set; }

        #region IEnumerable<GraphDataItem> Members

        public IEnumerator<GraphDataItem> GetEnumerator()
        {
            return mList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return mList.GetEnumerator();
        }

        #endregion
    }

}