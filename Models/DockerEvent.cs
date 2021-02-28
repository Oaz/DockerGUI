using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Docker.DotNet.Models;

namespace DockerGUI.Models
{
  public class DockerEvent
  {
    public DockerEvent(Message message)
    {
      Time = new DateTime(1970, 1, 1, 0,0,0,DateTimeKind.Utc).Add(new TimeSpan(message.TimeNano / 100));
      Type = EventTypes[message.Type];
      Action = Actions[message.Action];
      Subject = message.ID;
    }
    public DateTime Time { get; }
    public string Subject { get; }
    public DockerEventType Type { get; }
    public DockerAction Action { get; }

    static DockerEvent()
    {
      EventTypes = typeof(DockerEventType).GetEnumValues().Cast<DockerEventType>().ToDictionary(et => et.ToString().ToLower(), et => et);
      string ActionName(DockerAction dockerAction) => dockerAction.GetType().GetField(dockerAction.ToString())?.GetCustomAttribute<DataMemberAttribute>()?.Name!;
      Actions = typeof(DockerAction).GetEnumValues().Cast<DockerAction>().ToDictionary(a => ActionName(a), a => a);
    }

    private static readonly IDictionary<string, DockerEventType> EventTypes;
    private static readonly IDictionary<string, DockerAction> Actions;
  }
}