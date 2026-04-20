using System;
using System.Threading;
using System.Threading.Tasks;

namespace Samsung.OneUI.WinUI.Controls;

internal class OneUIContentDialogAsyncLock
{
	private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

	public async Task<IDisposable> LockAsync(CancellationTokenSource cts = null)
	{
		CancellationToken cancellationToken = cts?.Token ?? CancellationToken.None;
		await _semaphore.WaitAsync(cancellationToken);
		return new OneUIContentDialogAsyncLockReleaser(_semaphore);
	}
}
