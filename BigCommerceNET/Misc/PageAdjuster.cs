using System.Net;

namespace BigCommerceNET.Misc
{
    /// <summary>
    /// The page adjuster.
    /// </summary>
    public static class PageAdjuster
	{
        /// <summary>
        /// Gets the half page size.
        /// </summary>
        /// <param name="currentPageSize">The current page size.</param>
        /// <returns>An integer.</returns>
        public static int GetHalfPageSize( int currentPageSize )
		{
			return Math.Max( (int)Math.Floor( currentPageSize / 2d ), 1 );
		}

        /// <summary>
        /// Gets the next page index.
        /// </summary>
        /// <param name="currentPageInfo">The current page info.</param>
        /// <param name="newPageSize">The new page size.</param>
        /// <returns>An integer.</returns>
        public static int GetNextPageIndex( PageInfo currentPageInfo, int newPageSize )
		{
			var entitiesReceived = currentPageInfo.Size * ( currentPageInfo.Index - 1 );
			return (int)Math.Floor( entitiesReceived * 1.0 / newPageSize ) + 1;
		}

        /// <summary>
        /// Try adjust page if response too large.
        /// </summary>
        /// <param name="currentPageInfo">The current page info.</param>
        /// <param name="minPageSize">The min page size.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="newPageInfo">The new page info.</param>
        /// <returns>A bool.</returns>
        public static bool TryAdjustPageIfResponseTooLarge( PageInfo currentPageInfo, int minPageSize, Exception ex, out PageInfo newPageInfo )
		{
			newPageInfo = currentPageInfo;

			if ( IsResponseTooLargeToRead( ex ) )
			{
				var newPageSize = PageAdjuster.GetHalfPageSize( currentPageInfo.Size );
				if ( newPageSize >= minPageSize )
				{
					newPageInfo.Index = PageAdjuster.GetNextPageIndex( currentPageInfo, newPageSize );
					newPageInfo.Size = newPageSize;

					return true;
				}
			}

			return false;
		}

        /// <summary>
        /// Checks if is response too large to read.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>A bool.</returns>
        public static bool IsResponseTooLargeToRead( Exception ex )
		{
			if ( ex?.InnerException == null )
				return false;

			if ( ex.InnerException is IOException )
				return true;

			var webEx = ex.InnerException as WebException;
			if ( webEx != null )
			{
				return webEx.Status == WebExceptionStatus.ConnectionClosed;
			}

			return false;
		}
	}

	public struct PageInfo
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="PageInfo"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        public PageInfo( int index, int size )
		{
			this.Index = index;
			this.Size = size;
		}

        /// <summary>
        /// Gets or Sets the index.
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Gets or Sets the size.
        /// </summary>
        public int Size { get; set; }
	}
}