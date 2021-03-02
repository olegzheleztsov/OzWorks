namespace Oz.Algorithms.Matrices
{
    public class IntegerMatrixRegion : MatrixRegionBase<int>
    {
        public IntegerMatrixRegion(IntegerMatrix matrixBase, int startRow, int startColumn, int rows, int columns)
            : base(matrixBase, startRow, startColumn, rows, columns)
        {
        }

        public IntegerMatrixRegion(IntegerMatrixRegion matrixRegionBase, int offsetRow, int offsetColumn, int rows,
            int columns)
            : base(matrixRegionBase, offsetRow, offsetColumn, rows, columns)
        {
        }

        public static void AddRegions(IntegerMatrixRegion first, IntegerMatrixRegion second, IntegerMatrixRegion result)
        {
            for (var i = 0; i < first.Rows; i++)
            {
                for (var j = 0; j < first.Columns; j++)
                {
                    result[i, j] = first[i, j] + second[i, j];
                }
            }
        }

        public static void SubtractRegions(IntegerMatrixRegion first, IntegerMatrixRegion second,
            IntegerMatrixRegion result)
        {
            for (var i = 0; i < first.Rows; i++)
            {
                for (var j = 0; j < first.Columns; j++)
                {
                    result[i, j] = first[i, j] - second[i, j];
                }
            }
        }

        private static (IntegerMatrixRegion r00, IntegerMatrixRegion r01, IntegerMatrixRegion r10, IntegerMatrixRegion
            r11)
            DivideRegion(IntegerMatrixRegion region, int newSize)
        {
            return (new IntegerMatrixRegion(region, 0, 0, newSize, newSize),
                new IntegerMatrixRegion(region, 0, newSize, newSize, newSize),
                new IntegerMatrixRegion(region, newSize, 0, newSize, newSize),
                new IntegerMatrixRegion(region, newSize, newSize, newSize, newSize));
        }

        public static IntegerMatrixRegion MultiplyRegions(IntegerMatrixRegion first, IntegerMatrixRegion second,
            IntegerMatrixRegion result)
        {
            var n = first.Rows;
            if (n == 1)
            {
                result[0, 0] = first[0, 0] * second[0, 0];
            }
            else
            {
                var (a00, a01, a10, a11) = DivideRegion(first, n / 2);
                var (b00, b01, b10, b11) = DivideRegion(second, n / 2);
                var (c00, c01, c10, c11) = DivideRegion(result, n / 2);


                var temp = new IntegerMatrixRegion[8];
                for (var i = 0; i < 8; i++)
                {
                    temp[i] = new IntegerMatrixRegion(new IntegerMatrix(n / 2, n / 2), 0, 0, n / 2, n / 2);
                }

                AddRegions(MultiplyRegions(a00, b00, temp[0]),
                    MultiplyRegions(a01, b10, temp[1]), c00);
                AddRegions(MultiplyRegions(a00, b01, temp[2]),
                    MultiplyRegions(a01, b11, temp[3]), c01);
                AddRegions(MultiplyRegions(a10, b00, temp[4]),
                    MultiplyRegions(a11, b10, temp[5]), c10);
                AddRegions(MultiplyRegions(a10, b01, temp[6]),
                    MultiplyRegions(a11, b11, temp[7]), c11);
            }

            return result;
        }

        public static IntegerMatrixRegion StrassenFastMultiply(IntegerMatrixRegion first, IntegerMatrixRegion second,
            IntegerMatrixRegion result)
        {
            var n = first.Rows;
            if (n == 1)
            {
                result[0, 0] = first[0, 0] * second[0, 0];
            }
            else
            {
                var (a00, a01, a10, a11) = DivideRegion(first, n / 2);
                var (b00, b01, b10, b11) = DivideRegion(second, n / 2);
                var (c00, c01, c10, c11) = DivideRegion(result, n / 2);

                var sRegions = new IntegerMatrixRegion[10];
                for (var i = 0; i < sRegions.Length; i++)
                {
                    sRegions[i] = new IntegerMatrixRegion(new IntegerMatrix(n / 2, n / 2), 0, 0, n / 2, n / 2);
                }

                var pRegions = new IntegerMatrixRegion[7];
                for (var i = 0; i < pRegions.Length; i++)
                {
                    pRegions[i] = new IntegerMatrixRegion(new IntegerMatrix(n / 2, n / 2), 0, 0, n / 2, n / 2);
                }

                SubtractRegions(b01, b11, sRegions[0]);
                AddRegions(a00, a01, sRegions[1]);
                AddRegions(a10, a11, sRegions[2]);
                SubtractRegions(b10, b00, sRegions[3]);
                AddRegions(a00, a11, sRegions[4]);
                AddRegions(b00, b11, sRegions[5]);
                SubtractRegions(a01, a11, sRegions[6]);
                AddRegions(b10, b11, sRegions[7]);
                SubtractRegions(a00, a10, sRegions[8]);
                AddRegions(b00, b01, sRegions[9]);

                StrassenFastMultiply(a00, sRegions[0], pRegions[0]);
                StrassenFastMultiply(sRegions[1], b11, pRegions[1]);
                StrassenFastMultiply(sRegions[2], b00, pRegions[2]);
                StrassenFastMultiply(a11, sRegions[3], pRegions[3]);
                StrassenFastMultiply(sRegions[4], sRegions[5], pRegions[4]);
                StrassenFastMultiply(sRegions[6], sRegions[7], pRegions[5]);
                StrassenFastMultiply(sRegions[8], sRegions[9], pRegions[6]);

                AddRegions(pRegions[4], pRegions[3], c00);
                SubtractRegions(c00, pRegions[1], c00);
                AddRegions(c00, pRegions[5], c00);

                AddRegions(pRegions[0], pRegions[1], c01);

                AddRegions(pRegions[2], pRegions[3], c10);

                AddRegions(pRegions[4], pRegions[0], c11);
                SubtractRegions(c11, pRegions[2], c11);
                SubtractRegions(c11, pRegions[6], c11);
            }

            return result;
        }
    }
}