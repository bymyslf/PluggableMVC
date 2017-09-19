namespace PluginFramework
{
    using System;

    /// <summary>
	/// Abstract implementation of IDisposable.
	/// </summary>
	/// <remarks>
	/// Can also be used as a pattern for when inheriting is not possible.
	///
    /// See also: https://msdn.microsoft.com/en-us/library/b1yfkh5e%28v=vs.110%29.aspx
    /// See also: https://lostechies.com/chrispatterson/2012/11/29/idisposable-done-right/
    ///
    /// Note: if an object's ctor throws, it will never be disposed, and so if that ctor
    /// has allocated disposable objects, it should take care of disposing them.
	/// </remarks>
	public abstract class Disposable : IDisposable
    {
        private volatile bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Disposable()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            if (disposing)
            {
                DisposeResources();
            }
        }

        protected abstract void DisposeResources();
    }
}