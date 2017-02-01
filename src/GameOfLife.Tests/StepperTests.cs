using GameOfLife.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameOfLife.Tests
{
    public class StepperTests
    {
        [Fact]
        public void EmptyBoard_0_0_Test()
        {
            var gen0 = new bool[0, 0];

            var stepper = new Stepper();

            var gen1 = stepper.GetNextGeneration(gen0);

            Assert.Equal(0, gen1.GetLength(0));
            Assert.Equal(0, gen1.GetLength(1));
        }

        [Fact]
        public void EmptyBoard_5_0_Test()
        {
            var gen0 = new bool[5, 0];

            var stepper = new Stepper();

            var gen1 = stepper.GetNextGeneration(gen0);

            Assert.Equal(5, gen1.GetLength(0));
            Assert.Equal(0, gen1.GetLength(1));
        }

        [Fact]
        public void EmptyBoard_1_1_Test()
        {
            var gen0 = new bool[1, 1];

            var stepper = new Stepper();

            var gen1 = stepper.GetNextGeneration(gen0);

            Assert.Equal(1, gen1.GetLength(0));
            Assert.Equal(1, gen1.GetLength(1));

            Assert.False(gen1[0, 0]);
        }

        [Fact]
        public void FullBoard_1_1_Test()
        {
            var gen0 = new bool[1, 1];
            gen0[0, 0] = true;

            var stepper = new Stepper();

            var gen1 = stepper.GetNextGeneration(gen0);

            Assert.False(gen1[0, 0]);
        }

        [Fact]
        public void FullBoard_2_1_Test()
        {
            var gen0 = new bool[2, 1];
            gen0[0, 0] = true;
            gen0[1, 0] = true;

            var stepper = new Stepper();

            var gen1 = stepper.GetNextGeneration(gen0);

            Assert.False(gen1[0, 0]);
            Assert.False(gen1[1, 0]);
        }

        [Fact]
        public void FullBoard_2_2_Test()
        {
            var gen0 = new bool[2, 2];
            gen0[0, 0] = true;
            gen0[0, 1] = true;
            gen0[1, 0] = true;
            gen0[1, 1] = true;

            var stepper = new Stepper();

            var gen1 = stepper.GetNextGeneration(gen0);

            Assert.True(gen1[0, 0]);
            Assert.True(gen1[0, 1]);
            Assert.True(gen1[1, 0]);
            Assert.True(gen1[1, 1]);
        }

        [Fact]
        public void Board_2x2_3Lives_Test()
        {
            var gen0 = new bool[2, 2];
            gen0[0, 0] = true;
            gen0[0, 1] = true;
            gen0[1, 0] = true;
            gen0[1, 1] = false;

            var stepper = new Stepper();

            var gen1 = stepper.GetNextGeneration(gen0);

            //mindnem igazzá kell válnia
            Assert.True(gen1[0, 0]);
            Assert.True(gen1[0, 1]);
            Assert.True(gen1[1, 0]);
            Assert.True(gen1[1, 1]);
        }

        [Fact]
        public void Board_3x3_9Lives_Test()
        {
            var gen0 = new bool[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gen0[i, j] = true;
                }
            }

            var stepper = new Stepper();

            var gen1 = stepper.GetNextGeneration(gen0);

            //sarkok maradnak, többi meghal
            Assert.True(gen1[0, 0]);
            Assert.False(gen1[0, 1]);
            Assert.True(gen1[0, 2]);
            Assert.False(gen1[1, 0]);
            Assert.False(gen1[1, 1]);
            Assert.False(gen1[1, 2]);
            Assert.True(gen1[2, 0]);
            Assert.False(gen1[2, 1]);
            Assert.True(gen1[2, 2]);
        }

        [Fact]
        public void Board_5x5_BlinkerTest()
        {
            //     
            //  *  
            //  *  
            //  *  
            //     

            var gen0 = new bool[5, 5];
            for (int i = 0; i < 3; i++)
            {
                gen0[i + 1, 2] = true;
            }

            var stepper = new Stepper();

            var gen1 = stepper.GetNextGeneration(gen0);

            for (int i = 0; i < 5; i++)
            {
                Assert.False(gen1[0, i]);
                Assert.False(gen1[1, i]);
                Assert.False(gen1[3, i]);
                Assert.False(gen1[4, i]);
            }

            //középső sor:
            Assert.False(gen1[2, 0]);
            Assert.True(gen1[2, 1]);
            Assert.True(gen1[2, 2]);
            Assert.True(gen1[2, 3]);
            Assert.False(gen1[2, 4]);
        }
    }
}
