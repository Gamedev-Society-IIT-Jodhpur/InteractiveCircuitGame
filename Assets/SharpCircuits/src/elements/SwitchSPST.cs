using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpCircuit {

	public class SwitchSPST : CircuitElement {

		public Circuit.Lead leadA { get { return lead0; } }
		public Circuit.Lead leadB { get { return lead1; } }

		// position 0 == closed, position 1 == open
		protected int position { get; private set; }

		public bool IsOpen { get { return position == 1; } }

		protected int posCount;

		public SwitchSPST() : base() {
			position = 0;
			posCount = 2;
		}

		public SwitchSPST(bool mm) {
			position = (mm) ? 1 : 0;
			posCount = 2;
		}

		public virtual void toggle() {
			position++;
			if(position >= posCount)
				position = 0;
		}

		public virtual void setPosition(int pos) {
			position = pos;
			if(position >= posCount)
				position = 0;
		}

		public override void calculateCurrent() {
			if(position == 1)
				current = 0;
		}

		public override void stamp(Circuit sim) {
			if(position == 0)
				sim.stampVoltageSource(lead_node[0], lead_node[1], voltSource, 0);
		}

		public override int getVoltageSourceCount() {
			return (position == 1) ? 0 : 1;
		}

		/*public override void getInfo(String[] arr) {
			arr[0] = string.Empty;
			if(position == 1) {
				arr[1] = "open";
				arr[2] = "Vd = " + getVoltageDText(getVoltageDiff());
			} else {
				arr[1] = "closed";
				arr[2] = "V = " + getVoltageText(lead_volt[0]);
				arr[3] = "I = " + getCurrentDText(current);
			}
		}*/

		public override bool leadsAreConnected(int n1, int n2) {
			return position == 0;
		}

		public override bool isWire() {
			return true;
		}

	}
}