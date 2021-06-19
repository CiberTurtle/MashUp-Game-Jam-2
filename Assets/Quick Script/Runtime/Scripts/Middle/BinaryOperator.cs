using UnityEngine;
using UnityEngine.Events;

namespace QuickScript
{
	[AddComponentMenu("Quick Script/Middle/Binary Operator")]
	public class BinaryOperator : MonoBehaviour
	{
		public enum Operator
		{
			AND,
			OR,
			XOR
		}

		public Operator binaryOperator = Operator.AND;
		public void SetOperator(Operator binaryOperator) => this.binaryOperator = binaryOperator;

		public bool triggerImmediately = true;
		public void SetTriggerImmediately(bool triggerImmediately) => this.triggerImmediately = triggerImmediately;

		[Space]

		public UnityEvent onTrigger = new UnityEvent();

		bool triggerA = false;
		bool triggerB = false;

		public void Unset()
		{
			triggerA = false;
			triggerB = false;
		}

		public void TriggerA()
		{
			triggerA = true;

			if (triggerImmediately) TryTrigger();
		}

		public void TriggerB()
		{
			triggerA = true;

			if (triggerImmediately) TryTrigger();
		}

		public void TryTrigger()
		{
			switch (binaryOperator)
			{
				case Operator.AND: if (triggerA && triggerB) onTrigger.Invoke(); break;
				case Operator.OR: if (triggerA || triggerB) onTrigger.Invoke(); break;
				case Operator.XOR: if (triggerA ^ triggerB) onTrigger.Invoke(); break;
			}
		}
	}
}