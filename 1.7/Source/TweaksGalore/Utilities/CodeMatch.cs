using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace TweaksGalore
{
	public class CodeMatch
	{
		public string name;

		public List<OpCode> opcodes = new List<OpCode>();

		public List<object> operands = new List<object>();

		public List<Label> labels = new List<Label>();

		public List<ExceptionBlock> blocks = new List<ExceptionBlock>();

		public List<int> jumpsFrom = new List<int>();

		public List<int> jumpsTo = new List<int>();

		public Func<CodeInstruction, bool> predicate;

		public CodeMatch(OpCode? opcode = null, object operand = null, string name = null)
		{
			if (opcode.HasValue)
			{
				OpCode valueOrDefault = opcode.GetValueOrDefault();
				if (true)
				{
					opcodes.Add(valueOrDefault);
				}
			}
			if (operand != null)
			{
				operands.Add(operand);
			}
			this.name = name;
		}

		public CodeMatch(CodeInstruction instruction, string name = null)
			: this(instruction.opcode, instruction.operand, name)
		{
		}

		public CodeMatch(Func<CodeInstruction, bool> predicate, string name = null)
		{
			this.predicate = predicate;
			this.name = name;
		}

		internal bool Matches(List<CodeInstruction> codes, CodeInstruction instruction)
		{
			if (predicate != null)
			{
				return predicate(instruction);
			}
			if (opcodes.Count > 0 && !opcodes.Contains(instruction.opcode))
			{
				return false;
			}
			if (operands.Count > 0 && !operands.Contains(instruction.operand))
			{
				return false;
			}
			if (labels.Count > 0 && !labels.Intersect(instruction.labels).Any())
			{
				return false;
			}
			if (blocks.Count > 0 && !blocks.Intersect(instruction.blocks).Any())
			{
				return false;
			}
			if (jumpsFrom.Count > 0 && !jumpsFrom.Select((int index) => codes[index].operand).OfType<Label>().Intersect(instruction.labels)
				.Any())
			{
				return false;
			}
			if (jumpsTo.Count > 0)
			{
				object operand = instruction.operand;
				if (operand == null || operand.GetType() != typeof(Label))
				{
					return false;
				}
				Label label = (Label)operand;
				IEnumerable<int> second = from idx in Enumerable.Range(0, codes.Count)
										  where codes[idx].labels.Contains(label)
										  select idx;
				if (!jumpsTo.Intersect(second).Any())
				{
					return false;
				}
			}
			return true;
		}

		public override string ToString()
		{
			string text = "[";
			if (name != null)
			{
				text = text + name + ": ";
			}
			if (opcodes.Count > 0)
			{
				text = text + "opcodes=" + opcodes.Join() + " ";
			}
			if (operands.Count > 0)
			{
				text = text + "operands=" + operands.Join() + " ";
			}
			if (labels.Count > 0)
			{
				text = text + "labels=" + labels.Join() + " ";
			}
			if (blocks.Count > 0)
			{
				text = text + "blocks=" + blocks.Join() + " ";
			}
			if (jumpsFrom.Count > 0)
			{
				text = text + "jumpsFrom=" + jumpsFrom.Join() + " ";
			}
			if (jumpsTo.Count > 0)
			{
				text = text + "jumpsTo=" + jumpsTo.Join() + " ";
			}
			if (predicate != null)
			{
				text += "predicate=yes ";
			}
			return text.TrimEnd() + "]";
		}
	}
}
