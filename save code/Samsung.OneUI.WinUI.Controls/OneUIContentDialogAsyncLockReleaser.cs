using System;
using System.Threading;

namespace Samsung.OneUI.WinUI.Controls;

internal class OneUIContentDialogAsyncLockReleaser : IDisposable
{
	private readonly SemaphoreSlim _semaphore;

	public OneUIContentDialogAsyncLockReleaser(SemaphoreSlim semaphore)
	{
		_semaphore = semaphore;
	}

	public void Dispose()
	{
		Dispose(disposing: true);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
			_semaphore.Release();
		}
	}
}
