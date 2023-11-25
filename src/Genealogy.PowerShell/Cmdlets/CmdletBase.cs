using System;
using System.Linq;
using System.Management.Automation;
using Genealogy.PowerShell.Exceptions;
using Nito.AsyncEx;

namespace Genealogy.PowerShell.Cmdlets;

public abstract class CmdletBase : PSCmdlet
{
    private IServiceProvider? m_services = null;

    public CmdletBase()
    {
    }

    protected IServiceProvider Services { get => m_services ??= ServiceLocator.Instance; }

    protected override sealed void EndProcessing()
    {
        AsyncContext.Run(EndProcessingAsync);
    }

    protected virtual Task EndProcessingAsync() => Task.CompletedTask;

    protected bool IsParameterDefined(string parameterName)
    {
        return MyInvocation?.BoundParameters.ContainsKey(parameterName) == true;
    }
    protected override sealed void ProcessRecord()
    {
        AsyncContext.Run(ProcessRecordAsync);
    }

    protected virtual Task ProcessRecordAsync() => Task.CompletedTask;

    /// <exception cref="InvalidOptionException"></exception>
    protected T UserSelect<T>(string text, IReadOnlyList<T> items, Func<T, string> nameSelector)
    {
        Host.UI.WriteLine(text);
        for (int i = 0; i < items.Count; i++)
        {
            Host.UI.WriteLine($" {i + 1,2}) {nameSelector.Invoke(items[i])}");
        }
        Host.UI.WriteLine();
        Host.UI.Write("> ");

        if (int.TryParse(Host.UI.ReadLine(), out var selectedIndex))
        {
            selectedIndex--;
            if (selectedIndex >= 0 && selectedIndex < items.Count)
            {
                return items[selectedIndex];
            }
        }
        throw new InvalidOptionException();
    }
}
