using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpCircuit {

	public class TappedTransformer : CircuitElement {

		/// <summary>
		/// Primary Inductance (H)
		/// </summary>
		public double inductance { get; set; }

		/// <summary>
		/// Ratio
		/// </summary>
		public double ratio { get; set; }

		new public double[] current;

		private double[] a;
		private double[] curSourceValue, voltdiff;

		public TappedTransformer() : base() {
			inductance = 4;
			ratio = 1;
			current = new double[4];
			voltdiff = new double[3];
			curSourceValue = new double[3];
			a = new double[9];
		}

		public override int getLeadCount() {
			return 5;
		}

		public override void reset() {
			current[0] = current[1] = lead_volt[0] = lead_volt[1] = lead_volt[2] = lead_volt[3] = 0;
		}

		public override void stamp(Circuit sim) {
			// equations for transformer:
			// v1 = L1 di1/dt + M1 di2/dt + M1 di3/dt
			// v2 = M1 di1/dt + L2 di2/dt + M2 di3/dt
			// v3 = M1 di1/dt + M2 di2/dt + L2 di3/dt
			// we invert that to get:
			// di1/dt = a1 v1 + a2 v2 + a3 v3
			// di2/dt = a4 v1 + a5 v2 + a6 v3
			// di3/dt = a7 v1 + a8 v2 + a9 v3
			// integrate di1/dt using trapezoidal approx and we get:
			// i1(t2) = i1(t1) + dt/2 (i1(t1) + i1(t2))
			// = i1(t1) + a1 dt/2 v1(t1)+a2 dt/2 v2(t1)+a3 dt/2 v3(t3) +
			// a1 dt/2 v1(t2)+a2 dt/2 v2(t2)+a3 dt/2 v3(t3)
			// the norton equivalent of this for i1 is:
			// a. current source, I = i1(t1) + a1 dt/2 v1(t1) + a2 dt/2 v2(t1)
			// + a3 dt/2 v3(t1)
			// b. resistor, G = a1 dt/2
			// c. current source controlled by voltage v2, G = a2 dt/2
			// d. current source controlled by voltage v3, G = a3 dt/2
			// and similarly for i2
			//
			// first winding goes from node 0 to 1, second is from 2 to 3 to 4
			double l1 = inductance;
			double cc = .99;
			// double m1 = .999*Math.sqrt(l1*l2);
			// mutual inductance between two halves of the second winding
			// is equal to self-inductance of either half (slightly less
			// because the coupling is not perfect)
			// double m2 = .999*l2;
			// load pre-inverted matrix
			a[0] = (1 + cc) / (l1 * (1 + cc - 2 * cc * cc));
			a[1] = a[2] = a[3] = a[6] = 2 * cc / ((2 * cc * cc - cc - 1) * inductance * ratio);
			a[4] = a[8] = -4 * (1 + cc) / ((2 * cc * cc - cc - 1) * l1 * ratio * ratio);
			a[5] = a[7] = 4 * cc / ((2 * cc * cc - cc - 1) * l1 * ratio * ratio);
			int i;
			for(i = 0; i != 9; i++)
				a[i] *= sim.timeStep / 2;

			sim.stampConductance(lead_node[0], lead_node[1], a[0]);
			sim.stampVCCS(lead_node[0], lead_node[1], lead_node[2], lead_node[3], a[1]);
			sim.stampVCCS(lead_node[0], lead_node[1], lead_node[3], lead_node[4], a[2]);

			sim.stampVCCS(lead_node[2], lead_node[3], lead_node[0], lead_node[1], a[3]);
			sim.stampConductance(lead_node[2], lead_node[3], a[4]);
			sim.stampVCCS(lead_node[2], lead_node[3], lead_node[3], lead_node[4], a[5]);

			sim.stampVCCS(lead_node[3], lead_node[4], lead_node[0], lead_node[1], a[6]);
			sim.stampVCCS(lead_node[3], lead_node[4], lead_node[2], lead_node[3], a[7]);
			sim.stampConductance(lead_node[3], lead_node[4], a[8]);

			for(i = 0; i != 5; i++)
				sim.stampRightSide(lead_node[i]);
		}

		public override void beginStep(Circuit sim) {
			voltdiff[0] = lead_volt[0] - lead_volt[1];
			voltdiff[1] = lead_volt[2] - lead_volt[3];
			voltdiff[2] = lead_volt[3] - lead_volt[4];
			for(int i = 0; i != 3; i++) {
				curSourceValue[i] = current[i];
				for(int j = 0; j != 3; j++)
					curSourceValue[i] += a[i * 3 + j] * voltdiff[j];
			}
		}

		public override void step(Circuit sim) {
			sim.stampCurrentSource(lead_node[0], lead_node[1], curSourceValue[0]);
			sim.stampCurrentSource(lead_node[2], lead_node[3], curSourceValue[1]);
			sim.stampCurrentSource(lead_node[3], lead_node[4], curSourceValue[2]);
		}

		public override void calculateCurrent() {
			voltdiff[0] = lead_volt[0] - lead_volt[1];
			voltdiff[1] = lead_volt[2] - lead_volt[3];
			voltdiff[2] = lead_volt[3] - lead_volt[4];
			for(int i = 0; i != 3; i++) {
				current[i] = curSourceValue[i];
				for(int j = 0; j != 3; j++)
					current[i] += a[i * 3 + j] * voltdiff[j];
			}
		}

		/*public override void getInfo(String[] arr) {
			arr[0] = "transformer";
			arr[1] = "L = " + getUnitText(inductance, "H");
			arr[2] = "Ratio = " + ratio;
			// arr[3] = "I1 = " + getCurrentText(current1);
			arr[3] = "Vd1 = " + getVoltageText(lead_volt[0] - lead_volt[2]);
			// arr[5] = "I2 = " + getCurrentText(current2);
			arr[4] = "Vd2 = " + getVoltageText(lead_volt[1] - lead_volt[3]);
		}*/

		public override bool leadsAreConnected(int n1, int n2) {
			if(comparePair(n1, n2, 0, 1)) return true;
			if(comparePair(n1, n2, 2, 3)) return true;
			if(comparePair(n1, n2, 3, 4)) return true;
			if(comparePair(n1, n2, 2, 4)) return true;
			return false;
		}

	}
}