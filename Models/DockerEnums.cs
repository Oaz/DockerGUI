using System.Runtime.Serialization;

namespace DockerGUI.Models
{
  public enum DockerEventType
  {
    Container,
    Image,
    Volume,
    Network,
    Daemon,
    Plugin,
    Node,
    Service,
    Secret,
    Config
  }

  public enum DockerAction
  {
    [DataMember(Name = "attach")]
    Attach,
    [DataMember(Name = "commit")]
    Commit,
    [DataMember(Name = "connect")]
    Connect,
    [DataMember(Name = "copy")]
    Copy,
    [DataMember(Name = "create")]
    Create,
    [DataMember(Name = "delete")]
    Delete,
    [DataMember(Name = "destroy")]
    Destroy,
    [DataMember(Name = "detach")]
    Detach,
    [DataMember(Name = "die")]
    Die,
    [DataMember(Name = "disconnect")]
    Disconnect,
    [DataMember(Name = "exec_create")]
    ExecCreate,
    [DataMember(Name = "exec_detach")]
    ExecDetach,
    [DataMember(Name = "exec_start")]
    ExecStart,
    [DataMember(Name = "exec_die")]
    ExecDie,
    [DataMember(Name = "export")]
    Export,
    [DataMember(Name = "health_status")]
    HealthStatus,
    [DataMember(Name = "import")]
    Import,
    [DataMember(Name = "kill")]
    Kill,
    [DataMember(Name = "load")]
    Load,
    [DataMember(Name = "mount")]
    Mount,
    [DataMember(Name = "oom")]
    OutOfMemory,
    [DataMember(Name = "pause")]
    Pause,
    [DataMember(Name = "pull")]
    Pull,
    [DataMember(Name = "push")]
    Push,
    [DataMember(Name = "reload")]
    Reload,
    [DataMember(Name = "remove")]
    Remove,
    [DataMember(Name = "rename")]
    Rename,
    [DataMember(Name = "resize")]
    Resize,
    [DataMember(Name = "restart")]
    Restart,
    [DataMember(Name = "save")]
    Save,
    [DataMember(Name = "start")]
    Start,
    [DataMember(Name = "stop")]
    Stop,
    [DataMember(Name = "tag")]
    Tag,
    [DataMember(Name = "top")]
    Top,
    [DataMember(Name = "unmount")]
    Unmount,
    [DataMember(Name = "unpause")]
    Unpause,
    [DataMember(Name = "untag")]
    Untag,
    [DataMember(Name = "update")]
    Update,
    [DataMember(Name = "prune")]
    Prune
  }
}