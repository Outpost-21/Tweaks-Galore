using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace TweaksGalore
{
	public class CodeMatcher
	{
		private readonly ILGenerator generator;

		private readonly List<CodeInstruction> codes = new List<CodeInstruction>();

		private Dictionary<string, CodeInstruction> lastMatches = new Dictionary<string, CodeInstruction>();

		private string lastError;

		private bool lastUseEnd;

		private CodeMatch[] lastCodeMatches;

		public int Pos { get; private set; } = -1;


		public int Length => codes.Count;

		public bool IsValid => Pos >= 0 && Pos < Length;

		public bool IsInvalid => Pos < 0 || Pos >= Length;

		public int Remaining => Length - Math.Max(0, Pos);

		public ref OpCode Opcode => ref codes[Pos].opcode;

		public ref object Operand => ref codes[Pos].operand;

		public ref List<Label> Labels => ref codes[Pos].labels;

		public ref List<ExceptionBlock> Blocks => ref codes[Pos].blocks;

		public CodeInstruction Instruction => codes[Pos];

		private void FixStart()
		{
			Pos = Math.Max(0, Pos);
		}

		private void SetOutOfBounds(int direction)
		{
			Pos = ((direction > 0) ? Length : (-1));
		}

		public CodeMatcher()
		{
		}

		public CodeMatcher(IEnumerable<CodeInstruction> instructions, ILGenerator generator = null)
		{
			this.generator = generator;
			codes = instructions.Select((CodeInstruction c) => new CodeInstruction(c)).ToList();
		}

		public CodeMatcher Clone()
		{
			return new CodeMatcher(codes, generator)
			{
				Pos = Pos,
				lastMatches = lastMatches,
				lastError = lastError,
				lastUseEnd = lastUseEnd,
				lastCodeMatches = lastCodeMatches
			};
		}

		public CodeInstruction InstructionAt(int offset)
		{
			return codes[Pos + offset];
		}

		public List<CodeInstruction> Instructions()
		{
			return codes;
		}

		public IEnumerable<CodeInstruction> InstructionEnumeration()
		{
			return codes.AsEnumerable();
		}

		public List<CodeInstruction> Instructions(int count)
		{
			return (from c in codes.GetRange(Pos, count)
					select new CodeInstruction(c)).ToList();
		}

		public List<CodeInstruction> InstructionsInRange(int start, int end)
		{
			List<CodeInstruction> list = codes;
			if (start > end)
			{
				int num = start;
				start = end;
				end = num;
			}
			list = list.GetRange(start, end - start + 1);
			return list.Select((CodeInstruction c) => new CodeInstruction(c)).ToList();
		}

		public List<CodeInstruction> InstructionsWithOffsets(int startOffset, int endOffset)
		{
			return InstructionsInRange(Pos + startOffset, Pos + endOffset);
		}

		public static List<Label> DistinctLabels(IEnumerable<CodeInstruction> instructions)
		{
			return instructions.SelectMany((CodeInstruction instruction) => instruction.labels).Distinct().ToList();
		}

		public bool ReportFailure(MethodBase method, Action<string> logger)
		{
			if (IsValid)
			{
				return false;
			}
			logger(string.Format("{0} in {1}", lastError ?? "Unexpected code", method));
			return true;
		}

		public CodeMatcher SetInstruction(CodeInstruction instruction)
		{
			codes[Pos] = instruction;
			return this;
		}

		public CodeMatcher SetInstructionAndAdvance(CodeInstruction instruction)
		{
			SetInstruction(instruction);
			Pos++;
			return this;
		}

		public CodeMatcher Set(OpCode opcode, object operand)
		{
			Opcode = opcode;
			Operand = operand;
			return this;
		}

		public CodeMatcher SetAndAdvance(OpCode opcode, object operand)
		{
			Set(opcode, operand);
			Pos++;
			return this;
		}

		public CodeMatcher SetOpcodeAndAdvance(OpCode opcode)
		{
			Opcode = opcode;
			Pos++;
			return this;
		}

		public CodeMatcher SetOperandAndAdvance(object operand)
		{
			Operand = operand;
			Pos++;
			return this;
		}

		public CodeMatcher CreateLabel(out Label label)
		{
			label = generator.DefineLabel();
			Labels.Add(label);
			return this;
		}

		public CodeMatcher CreateLabelAt(int position, out Label label)
		{
			label = generator.DefineLabel();
			AddLabelsAt(position, new Label[1] { label });
			return this;
		}

		public CodeMatcher AddLabels(IEnumerable<Label> labels)
		{
			Labels.AddRange(labels);
			return this;
		}

		public CodeMatcher AddLabelsAt(int position, IEnumerable<Label> labels)
		{
			codes[position].labels.AddRange(labels);
			return this;
		}

		public CodeMatcher SetJumpTo(OpCode opcode, int destination, out Label label)
		{
			CreateLabelAt(destination, out label);
			Set(opcode, label);
			return this;
		}

		public CodeMatcher Insert(params CodeInstruction[] instructions)
		{
			codes.InsertRange(Pos, instructions);
			return this;
		}

		public CodeMatcher Insert(IEnumerable<CodeInstruction> instructions)
		{
			codes.InsertRange(Pos, instructions);
			return this;
		}

		public CodeMatcher InsertBranch(OpCode opcode, int destination)
		{
			CreateLabelAt(destination, out var label);
			codes.Insert(Pos, new CodeInstruction(opcode, label));
			return this;
		}

		public CodeMatcher InsertAndAdvance(params CodeInstruction[] instructions)
		{
			instructions.Do(delegate (CodeInstruction instruction)
			{
				Insert(instruction);
				Pos++;
			});
			return this;
		}

		public CodeMatcher InsertAndAdvance(IEnumerable<CodeInstruction> instructions)
		{
			instructions.Do(delegate (CodeInstruction instruction)
			{
				InsertAndAdvance(instruction);
			});
			return this;
		}

		public CodeMatcher InsertBranchAndAdvance(OpCode opcode, int destination)
		{
			InsertBranch(opcode, destination);
			Pos++;
			return this;
		}

		public CodeMatcher RemoveInstruction()
		{
			codes.RemoveAt(Pos);
			return this;
		}

		public CodeMatcher RemoveInstructions(int count)
		{
			codes.RemoveRange(Pos, Pos + count - 1);
			return this;
		}

		public CodeMatcher RemoveInstructionsInRange(int start, int end)
		{
			if (start > end)
			{
				int num = start;
				start = end;
				end = num;
			}
			codes.RemoveRange(start, end - start + 1);
			return this;
		}

		public CodeMatcher RemoveInstructionsWithOffsets(int startOffset, int endOffset)
		{
			RemoveInstructionsInRange(Pos + startOffset, Pos + endOffset);
			return this;
		}

		public CodeMatcher Advance(int offset)
		{
			Pos += offset;
			if (!IsValid)
			{
				SetOutOfBounds(offset);
			}
			return this;
		}

		public CodeMatcher Start()
		{
			Pos = 0;
			return this;
		}

		public CodeMatcher End()
		{
			Pos = Length - 1;
			return this;
		}

		public CodeMatcher SearchForward(Func<CodeInstruction, bool> predicate)
		{
			return Search(predicate, 1);
		}

		public CodeMatcher SearchBack(Func<CodeInstruction, bool> predicate)
		{
			return Search(predicate, -1);
		}

		private CodeMatcher Search(Func<CodeInstruction, bool> predicate, int direction)
		{
			FixStart();
			while (IsValid && !predicate(Instruction))
			{
				Pos += direction;
			}
			lastError = (IsInvalid ? ("Cannot find " + predicate) : null);
			return this;
		}

		public CodeMatcher MatchForward(bool useEnd, params CodeMatch[] matches)
		{
			return Match(matches, 1, useEnd);
		}

		public CodeMatcher MatchBack(bool useEnd, params CodeMatch[] matches)
		{
			return Match(matches, -1, useEnd);
		}

		private CodeMatcher Match(CodeMatch[] matches, int direction, bool useEnd)
		{
			FixStart();
			while (IsValid)
			{
				lastUseEnd = useEnd;
				lastCodeMatches = matches;
				if (MatchSequence(Pos, matches))
				{
					if (useEnd)
					{
						Pos += matches.Count() - 1;
					}
					break;
				}
				Pos += direction;
			}
			lastError = (IsInvalid ? ("Cannot find " + matches.Join()) : null);
			return this;
		}

		public CodeMatcher Repeat(Action<CodeMatcher> matchAction, Action<string> notFoundAction = null)
		{
			int num = 0;
			if (lastCodeMatches == null)
			{
				throw new InvalidOperationException("No previous Match operation - cannot repeat");
			}
			while (IsValid)
			{
				matchAction(this);
				MatchForward(lastUseEnd, lastCodeMatches);
				num++;
			}
			lastCodeMatches = null;
			if (num == 0)
			{
				notFoundAction?.Invoke(lastError);
			}
			return this;
		}

		public CodeInstruction NamedMatch(string name)
		{
			return lastMatches[name];
		}

		private bool MatchSequence(int start, CodeMatch[] matches)
		{
			if (start < 0)
			{
				return false;
			}
			lastMatches = new Dictionary<string, CodeInstruction>();
			foreach (CodeMatch codeMatch in matches)
			{
				if (start >= Length || !codeMatch.Matches(codes, codes[start]))
				{
					return false;
				}
				if (codeMatch.name != null)
				{
					lastMatches.Add(codeMatch.name, codes[start]);
				}
				start++;
			}
			return true;
		}
	}
}
