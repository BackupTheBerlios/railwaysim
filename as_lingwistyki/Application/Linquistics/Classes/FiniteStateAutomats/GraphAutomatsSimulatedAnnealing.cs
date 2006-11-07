using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Collections.Generic;

namespace Linquistics
{
    partial class GraphAutomats
    {
        private static double startTemperature = 1.0;
        private static double temperatureFactor = 0.75;
        private static double currentTemperature = 0;
        private List<Node> bestNodes = null;
        private static int trialsNumber = 300;
        private static int trialsCount = 0;
        private static double lambdaDistanceNode = 0.5;
        private static double lambdaDistanceBorder = 0.7;
        private static double lambdaCrossings = 1.0;
        private static Random randomGenerator =  null;
        private static GraphAutomats graphNew =null;
        private static float currentRadius = 1.0f;
        private static float startRadius = 1.0f;
        private static double energyMax = 0.1;


        private static double EPSILON = 0.00000001;
        private static GraphAutomats bestGraph = null;
        private static double bestEnergy = 0.0;
        private void generateRandomStartedPoint(Rectangle border)
        {
            int i = 0;
            for (i = 0; i < nodes.Count; i++)
            {
                nodes[i].RandomizeLocation(border);
            }
            trialsNumber = 30 * nodes.Count; //set value proportional to nodes number

        }
        private bool simulateAnnealing(Rectangle border)
        {
            double bestEnergyExplicit = 0; 
            trialsCount = trialsNumber;
            currentTemperature = startTemperature;
            graphNew = new GraphAutomats();
            currentRadius = startRadius;
            

            this.generateRandomStartedPoint(border);
            graphNew.copyNodesAndEdges(this);
            bestGraph = new GraphAutomats();
            bestGraph.copyNodesAndEdges(this);

            double oldStateEnergy = this.calculateEnergy();
            bestEnergy = oldStateEnergy;
            double tempEn = graphNew.calculateEnergy();
            double newStateEnergy = 0;
            bestNodes = this.nodes;
            while (trialsCount > 0 && oldStateEnergy > energyMax)
            {
                
                graphNew.transformIntoNeighbor();
                newStateEnergy = graphNew.calculateEnergy();
                if (newStateEnergy < bestEnergy)
                {
                    bestGraph.copyNodesAndEdges(graphNew);
                    bestEnergy = newStateEnergy;
                    bestEnergyExplicit = bestGraph.calculateEnergy();
                }
                double randomNum = randomGenerator.NextDouble();
                double boltzmanNum = Math.Exp((oldStateEnergy - newStateEnergy) / currentTemperature);
                if (randomNum < boltzmanNum)
                {
                    oldStateEnergy = newStateEnergy;
                    this.copyNodesAndEdges(graphNew);
                }
                trialsCount--;
                currentTemperature *= temperatureFactor;
                currentRadius *= (float)temperatureFactor;
            }
            double finalEnergy = bestGraph.calculateEnergy();
            bestGraph.actualizeNodesScreenPositions();
            this.nodes = bestGraph.nodes;
            this.edges = bestGraph.edges;
            return true;
        }
        private double calculateEnergy()
        {
            double functVal = 0;
            functVal += lambdaDistanceNode/sumNodeDistances(); //first condition - distances between nodes
            functVal += lambdaDistanceBorder / sumBorderDist();
            functVal += lambdaCrossings * (float)calculateCrossings();
            return functVal;
        }
        private void transformIntoNeighbor()
        {
            int which = randomGenerator.Next(this.nodes.Count);
            double rsin=randomGenerator.NextDouble();
            double rcos=randomGenerator.NextDouble();
            double newRadius=randomGenerator.NextDouble()*currentRadius;
            float newx = (float) (rcos * newRadius) + this.nodes[which].LogicalPosition.X;
            float newy = (float)(rsin * newRadius) + this.nodes[which].LogicalPosition.X;
            this.nodes[which].LogicalPosition = new PointF(newx, newy);

        }
        private void copyNodesAndEdges(GraphAutomats patternGraph)
        {
           this.nodes.Clear();
           this.edges.Clear();
           int k = 0;
            int ind1=0,ind2=0;

            for (int i = 0; i < patternGraph.nodes.Count; i++)
            {
                Node newNode = new Node(patternGraph.nodes[i]);
                this.nodes.Add(newNode);
            }
            for (int i = 0; i < patternGraph.edges.Count; i++)
            {
                ind1 = patternGraph.nodes.IndexOf(patternGraph.edges[i].BeginNode);
                ind2 = patternGraph.nodes.IndexOf(patternGraph.edges[i].EndNode);
                Edge newEdge = new Edge(nodes[ind1], nodes[ind2], patternGraph.edges[i].EdgeChars);
                this.edges.Add(newEdge);
            }
        }
        

        private double sumNodeDistances()
        {
            int k=0;
            double sum = 0;
            
            for (int i = 0; i < nodes.Count; i++)
            {
                for (k = i + 1; k < nodes.Count; k++)
                {
                    sum+= Math.Sqrt(Math.Pow(nodes[i].LogicalPosition.X-nodes[k].LogicalPosition.X,2)+Math.Pow(nodes[i].LogicalPosition.Y-nodes[k].LogicalPosition.Y,2));
                }
            }
            if (sum == 0) sum = EPSILON;
            return sum;
        }
        private double sumBorderDist()
        {
            double sum=0;
            
            for (int k = 0; k < nodes.Count; k++)
            {
                sum += nodes[k].ReturnDistnacesSum();
            }
            if (sum == 0) sum = EPSILON;
            return sum;
        }
        private double detFunction(PointF p, PointF q, PointF r)
        {
            return p.X*q.Y+q.X*r.Y+r.X*p.Y-q.X*p.Y-p.X*r.Y-r.X*q.Y;
        }
        public int calculateCrossings()
        {
            int k=0;
            int crosNum=0;
            for (int i = 0; i < edges.Count; i++)
            {
                for (k = i + 1; k < edges.Count; k++)
                {
                    if (edges[i].BeginNode != edges[k].BeginNode &&
                        edges[i].BeginNode != edges[k].EndNode &&
                        edges[i].EndNode != edges[k].BeginNode &&
                        edges[i].EndNode != edges[k].EndNode)
                    {
                        double det1=detFunction(edges[i].BeginNode.LogicalPosition, edges[i].EndNode.LogicalPosition, edges[k].BeginNode.LogicalPosition);
                        double det2=detFunction(edges[i].BeginNode.LogicalPosition, edges[i].EndNode.LogicalPosition, edges[k].EndNode.LogicalPosition);
                        double det3=detFunction(edges[k].BeginNode.LogicalPosition, edges[k].EndNode.LogicalPosition, edges[i].BeginNode.LogicalPosition);
                        double det4=detFunction(edges[k].BeginNode.LogicalPosition, edges[k].EndNode.LogicalPosition, edges[i].EndNode.LogicalPosition);
                        if (Math.Sign(det1)!=Math.Sign(det2)
                            && Math.Sign(det3) != Math.Sign(det3))
                        {
                            crosNum++;
                        }
                       // else if(Math.Abs(det1-det2)
                      //  {

                      //  }
                    }
                }
            }
            return crosNum;
        }


    }
}