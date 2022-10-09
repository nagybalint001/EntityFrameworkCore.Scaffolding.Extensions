using EntityFrameworkCoreScaffoldingVSExtension.Models;
using EntityFrameworkCoreScaffoldingVSExtension.Services;

using Microsoft.VisualStudio.PlatformUI;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EntityFrameworkCoreScaffoldingVSExtension.Views;

public class ScaffoldingSettingsVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    
    public Action CloseAction { get; set; }

    public string DataSource
    {
        get { return _dataSource; }
        set
        {
            _dataSource = value;
            OnPropertyChanged();
        }
    }

    public string Provider
    {
        get { return _provider; }
        set
        {
            _provider = value;
            OnPropertyChanged();
        }
    }

    public string ContextName
    {
        get { return _contextName; }
        set
        {
            _contextName = value;
            OnPropertyChanged();
        }
    }

    public string ContextDirectory
    {
        get { return _contextDirectory; }
        set
        {
            _contextDirectory = value;
            OnPropertyChanged();
        }
    }

    public string OutputDirectory
    {
        get { return _outputDirectory; }
        set
        {
            _outputDirectory = value;
            OnPropertyChanged();
        }
    }

    public string ContextNamespace
    {
        get { return _contextNamespace; }
        set
        {
            _contextNamespace = value;
            OnPropertyChanged();
        }
    }

    public string Namespace
    {
        get { return _namespace; }
        set
        {
            _namespace = value;
            OnPropertyChanged();
        }
    }

    public string Schemas
    {
        get { return _schemas; }
        set
        {
            _schemas = value;
            OnPropertyChanged();
        }
    }

    public string Tables
    {
        get { return _tables; }
        set
        {
            _tables = value;
            OnPropertyChanged();
        }
    }

    public string ProjectDirectory
    {
        get { return _projectDirectory; }
        set
        {
            _projectDirectory = value;
            OnPropertyChanged();
        }
    }

    public string StartupProjectDirectory
    {
        get { return _startupProjectDirectory; }
        set
        {
            _startupProjectDirectory = value;
            OnPropertyChanged();
        }
    }

    public string TargetFramework
    {
        get { return _targetFramework; }
        set
        {
            _targetFramework = value;
            OnPropertyChanged();
        }
    }

    public string Configuration
    {
        get { return _configuration; }
        set
        {
            _configuration = value;
            OnPropertyChanged();
        }
    }

    public string TargetRuntime
    {
        get { return _targetRuntime; }
        set
        {
            _targetRuntime = value;
            OnPropertyChanged();
        }
    }

    public bool DataAnnotations
    {
        get { return _dataAnnotations; }
        set
        {
            _dataAnnotations = value;
            OnPropertyChanged();
        }
    }

    public bool UseDatabaseNames
    {
        get { return _useDatabaseNames; }
        set
        {
            _useDatabaseNames = value;
            OnPropertyChanged();
        }
    }

    public bool NoOnConfiguring
    {
        get { return _noOnConfiguring; }
        set
        {
            _noOnConfiguring = value;
            OnPropertyChanged();
        }
    }

    public bool NoPluralize
    {
        get { return _noPluralize; }
        set
        {
            _noPluralize = value;
            OnPropertyChanged();
        }
    }

    public bool Force
    {
        get { return _force; }
        set
        {
            _force = value;
            OnPropertyChanged();
        }
    }

    public bool NoBuild
    {
        get { return _noBuild; }
        set
        {
            _noBuild = value;
            OnPropertyChanged();
        }
    }

    public bool Verbose
    {
        get { return _verbose; }
        set
        {
            _verbose = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand SaveCommand { get; }

    public DelegateCommand CancelCommand { get; }

    public ScaffoldingSettingsVM(ScaffoldingSettings model)
    {
        DataSource = model.DataSource;
        Provider = model.Provider;
        ContextName = model.ContextName;
        ContextDirectory = model.ContextDirectory;
        OutputDirectory = model.OutputDirectory;
        ContextNamespace = model.ContextNamespace;
        Namespace = model.Namespace;
        Schemas = model.Schemas;
        Tables = model.Tables;
        ProjectDirectory = model.ProjectDirectory;
        StartupProjectDirectory = model.StartupProjectDirectory;
        TargetFramework = model.TargetFramework;
        Configuration = model.Configuration;
        TargetRuntime = model.TargetRuntime;
        DataAnnotations = model.DataAnnotations;
        UseDatabaseNames = model.UseDatabaseNames;
        NoOnConfiguring = model.NoOnConfiguring;
        NoPluralize = model.NoPluralize;
        Force = model.Force;
        NoBuild = model.NoBuild;
        Verbose = model.Verbose;

        SaveCommand = new DelegateCommand(OnSave);
        CancelCommand = new DelegateCommand(OnCancel);
    }

    public ScaffoldingSettings ToModel()
    {
        return new ScaffoldingSettings()
        {
            DataSource = DataSource,
            Provider = Provider,
            ContextName = ContextName,
            ContextDirectory = ContextDirectory,
            OutputDirectory = OutputDirectory,
            ContextNamespace = ContextNamespace,
            Namespace = Namespace,
            Schemas = Schemas,
            Tables = Tables,
            ProjectDirectory = ProjectDirectory,
            StartupProjectDirectory = StartupProjectDirectory,
            TargetFramework = TargetFramework,
            Configuration = Configuration,
            TargetRuntime = TargetRuntime,
            DataAnnotations = DataAnnotations,
            UseDatabaseNames = UseDatabaseNames,
            NoOnConfiguring = NoOnConfiguring,
            NoPluralize = NoPluralize,
            Force = Force,
            NoBuild = NoBuild,
            Verbose = Verbose,
        };
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "VSTHRD100:Avoid async void methods", Justification = "Command handler")]
    private async void OnSave()
    {
        var model = ToModel();
        await ConfigurationService.SaveSettingsAsync(model);
        CloseAction();
    }

    private void OnCancel()
    {
        CloseAction();
    }

    private string _dataSource;
    private string _provider;
    private string _contextName;
    private string _contextDirectory;
    private string _outputDirectory;
    private string _contextNamespace;
    private string _namespace;
    private string _schemas;
    private string _tables;
    private string _projectDirectory;
    private string _startupProjectDirectory;
    private string _targetFramework;
    private string _configuration;
    private string _targetRuntime;
    private bool _dataAnnotations;
    private bool _useDatabaseNames;
    private bool _noOnConfiguring;
    private bool _noPluralize;
    private bool _force;
    private bool _noBuild;
    private bool _verbose;

    private void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
