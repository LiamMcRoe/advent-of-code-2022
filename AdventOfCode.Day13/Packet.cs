using System.Text.Json.Nodes;

namespace AdventOfCode.Day13
{
	public class Packet
	{
		public Packet(string packetDefinition)
		{
			ParsedPacket = JsonNode.Parse(packetDefinition).AsArray();
		}

		public JsonArray ParsedPacket { get; init; }

		public bool IsLessThan(Packet otherPacket) => CompareArrays(this.ParsedPacket, otherPacket.ParsedPacket) == true;

		private static bool? CompareArrays(JsonArray localArray, JsonArray otherArray)
		{
			for (int i = 0; i < localArray.Count; i++)
			{
				if (otherArray.Count == i) return false;
				var equal = CompareNodes(localArray[i], otherArray[i]);
				if (equal.HasValue) return equal.Value;
			}

			if (localArray.Count < otherArray.Count) return true;
			return null;
		}

		private static bool? CompareNodes(JsonNode localNode, JsonNode otherNode)
		{
			if (localNode is JsonValue localVal && otherNode is JsonValue otherVal)
			{
				var localInt = localVal.GetValue<int>();
				var otherInt = otherVal.GetValue<int>();
				return localInt == otherInt ? null : localInt < otherInt;
			}
			
			return CompareArrays(ToJsonArray(localNode), ToJsonArray(otherNode));
		}

		private static JsonArray ToJsonArray(JsonNode node) =>
			node is JsonValue value ? new JsonArray { value.GetValue<int>() } : node.AsArray();
	}
}
