namespace EntityFrameworkCoreScaffoldingVSExtension.Models;

public class ScaffoldingSettings
{
    public string DataSource { get; set; }
    public string Provider { get; set; }
    public string ContextName { get; set; }
    public string ContextDirectory { get; set; }
    public string OutputDirectory { get; set; }
    public string ContextNamespace { get; set; }
    public string Namespace { get; set; }
    public string Schemas { get; set; }
    public string Tables { get; set; }
    public string ProjectDirectory { get; set; }
    public string StartupProjectDirectory { get; set; }
    public string TargetFramework { get; set; }
    public string Configuration { get; set; }
    public string TargetRuntime { get; set; }
    public bool DataAnnotations { get; set; }
    public bool UseDatabaseNames { get; set; }
    public bool NoOnConfiguring { get; set; }
    public bool NoPluralize { get; set; }
    public bool Force { get; set; }
    public bool NoBuild { get; set; }
    public bool Verbose { get; set; }
}
