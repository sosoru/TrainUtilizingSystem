using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Tus.TransControl.Base
{
    /// <summary>
    ///     ひとつの駆動源を持ち，絶縁されたセクションを表す．（having one motor, being terminated by a isolator)
    /// </summary>
    [DataContract]
    public class ControlUnit
    {
        [DataMember]
        public IList<Block> Blocks { get; set; }

        public Block ControlBlock
        {
            get { return Blocks.First(b => b.HasMotor); }
        }

        public IEnumerable<Block> HaltableBlocks
        {
            get
            {
                foreach (Block b in Blocks)
                {
                    if (b == Blocks.First())
                        yield return b; // stop at entrance of the blocks this class govern

                    if (b.HasSensor)
                        yield return b; // stop by a IR sensor
                }
            }
        }

        public bool CanBeAllocated
        {
            get { return !this.IsBlocked; }
        }

        public bool IsBlocked
        {
            get { return Blocks.Any(b => b.IsBlocked); }
        }

        public void Allocate()
        {
            Blocks.ForEach(b =>
                               {
                                   if (b.IsBlocked)
                                       throw new InvalidOperationException(
                                           "Allocate() tried to allocate a blocked block");

                                   b.IsBlocked = true;
                               });
        }

        public void Release()
        {
            Blocks.ForEach(b =>
                               {
                                   if (!b.IsBlocked)
                                       throw new InvalidOperationException("Release() tried to relase a released block");

                                   b.IsBlocked = false;
                               });
        }
    }

    /// <summary>
    ///     列車の進むべき経路を保持するクラス．ControlUnitContainerのIEnumeratableファクトリに相当．
    /// </summary>
    [DataContract]
    public class RouteOrder
    {
        #region constructor

        public RouteOrder(Block[] blocks)
        {
            Blocks = blocks;
            Units = new ReadOnlyCollection<ControlUnit>(locked_blocks.ToArray());
        }

        /// <summary>
        /// 単一シートとブロック名からRouteOrderを初期化
        /// </summary>
        /// <param name="sheet">使用するシート</param>
        /// <param name="names">ブロック名の配列</param>
        public RouteOrder(BlockSheet sheet, IEnumerable<string> names)
            : this(names.Select(s => sheet.InnerBlocks.First(b => b.Name == s)).ToArray())
        {
        }

        #endregion

        #region properties

        /// <summary>
        /// RouteOrderの名前
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// ルートを構成しているブロックの物理的な極性．
        /// +方向が時計回りに配置されるときPositive．反時計回りの時Negative．
        /// 配置の詳細はマニュアル参照のこと
        /// </summary>
        public BlockPolar Polar { get; set; }

        /// <summary>
        /// RouteOrderがエンドレス（ループ状）であるかどうか．
        /// </summary>
        public bool IsRepeatable { get; set; }

        /// <summary>
        /// RouteOrderを構成するBlock．代入不可・変更不可
        /// </summary>
        public IReadOnlyList<Block> Blocks { get; private set; }

        /// <summary>
        /// RouteOrderを構成するControlUnit．代入不可・変更不可
        /// </summary>
        public IReadOnlyList<ControlUnit> Units { get; private set; }

        //todo : move logic to ControlUnit
        private IEnumerable<ControlUnit> locked_blocks
        {
            get
            {
                var l = new List<Block>();

                foreach (Block b in Blocks)
                {
                    l.Add(b);
                    if (b.IsIsolated)
                    {
                        var cr = new ControlUnit { Blocks = new List<Block>(l) };

                        yield return cr;
                        l.Clear();
                    }
                }

                if (l.Count > 0)
                    yield return new ControlUnit { Blocks = new List<Block>(l) };

                //todo : throw exception (not terminated by a block having a sensor
            }
        }

        #endregion

        #region ToString

        private string GetBlockExpression(Block blk)
        {
            string exp = ":";
            if (blk.HasMotor)
                exp += "m";

            if (blk.HasSwitch)
                exp += "s";

            if (blk.HasSensor)
                exp += "e";

            if (blk.IsIsolated)
                exp += "i";

            if (exp.Length == 1)
                exp = "";

            return blk.Name + exp;
        }

        public string ToString()
        {
            string strunits = Units.Aggregate("", (ac, cntrt) =>
                                                  ac + "(" +
                                                  cntrt.Blocks.Aggregate("", (a, b) => a + GetBlockExpression(b) + " ")
                                                       .Trim() + ") ")
                                   .Trim();
            return string.Format("{0} : {1}", Name, strunits);
        }

        #endregion

        #region methods

        private ControlUnitContainer createContainer(int index)
        {
            return new ControlUnitContainer(this, index);
        }

        /// <summary>
        /// ControlUnitContainerの作成
        /// </summary>
        /// <param name="unit">作成するControlUnit</param>
        /// <returns>作成したControllUnitContainer．</returns>
        /// <exception cref="KeyNotFoundException">unitがこのRouteOrder内から見つからない場合に送出されます</exception>
        public ControlUnitContainer CreateContainer(ControlUnit unit)
        {
            var ind = Units.Select((b, i) => new { unit = b, ind = i }).First(_ => _.unit == unit).ind;

            if (ind < 0)
                throw new KeyNotFoundException("block is notfound");

            ControlUnitContainer container = createContainer(ind);
            return container;
        }

        /// <summary>
        /// ControlUnitの検索．指定されたBlockが含まれるControlUnitを返却
        /// </summary>
        /// <param name="blk">検索するBlock</param>
        /// <returns>blkが含まれるControlUnit</returns>
        /// <exception cref="IndexOutOfRangeException">blkが見つからないとき送出</exception>
        public ControlUnit SearchUnit(Block blk)
        {
            var blocklist = Units.Select((r, i) => new { ind = i, unit = r }).ToArray();
            var blockunit = blocklist.FirstOrDefault(u => u.unit.Blocks.Contains(blk));

            if (blockunit == null)
                throw new IndexOutOfRangeException("block not found");

            return blockunit.unit;
        }

        /// <summary>
        /// 指定されたBlockが操作しているControlUnitを返す．ControlUnit内のControlBlockのみを比較
        /// </summary>
        /// <param name="parentBlock">ControlUnitを操作するBlock</param>
        /// <returns>parentBlockが操作しているControlUnit．見つからなければnull</returns>
        public ControlUnit GetControlingUnit(Block parentBlock)
        {
            return Units.FirstOrDefault(b => b.ControlBlock == parentBlock);
        }

        /// <summary>
        /// BlockのRouteSegmentを作成．
        /// </summary>
        /// <param name="blk"></param>
        /// <returns></returns>
        [Obsolete]
        public RouteSegment GetSegment(Block blk)
        {
            var ind = this.Blocks.Select((b, i) => new { blk = b, ind = i }).First(_ => _.blk == blk).ind;

            if (ind == 0)
            {
                Block first = null;
                if (this.IsRepeatable)
                    first = this.Blocks.Last();

                return new RouteSegment(first, this.Blocks[ind + 1]);
            }
            else if (ind == this.Blocks.Count - 1)
            {
                Block last = null;
                if (this.IsRepeatable)
                    last = this.Blocks.First();

                return new RouteSegment(this.Blocks[ind - 1], last);
            }
            else
            {
                return new RouteSegment(this.Blocks[ind - 1], this.Blocks[ind + 1]);
            }
        }

        /// <summary>
        /// 指定されたControlUnitから列挙を開始．各要素はControlUnitContainerとして返却．
        /// ControlUnitContainer.UnitからControlUnit取れます.
        /// IsRepeatableがtrueの時，Unitsの終点ユニットの次は始点を返そうとし，無限要素個のIEnumerableとして振る舞います．
        /// </summary>
        /// <param name="from">列挙の始点となるControlUnit</param>
        /// <returns>fromを始点とするIEnumerable</returns>
        public IEnumerable<ControlUnitContainer> Enumerate(ControlUnit from)
        {
            ControlUnitContainer container = CreateContainer(@from);

            yield return container;
            while (true)
            {
                ControlUnitContainer next = container.Next;
                if (next != null)
                {
                    yield return next;
                    container = next;
                }
                else // nullの時はNext存在しない
                    yield break;
            }
        }

        #endregion
    }

    [DataContract]
    public class ControlUnitContainer
    {
        public ControlUnitContainer(RouteOrder order, int index)
        {
            Order = order;
            Index = index;
        }

        public int Index { get; set; }
        public RouteOrder Order { get; set; }

        public ControlUnit Unit
        {
            get { return Order.Units[Index]; }
        }

        public ControlUnitContainer Next
        {
            get { return GetNeighbor(1); }
        }

        public ControlUnitContainer Before
        {
            get { return GetNeighbor(-1); }
        }

        public ControlUnitContainer GetNeighbor(int distance)
        {
            distance %= Order.Units.Count;

            if (distance > 0)
            {
                int nextind = Index + distance;
                if (nextind >= Order.Units.Count)
                {
                    if (Order.IsRepeatable)
                    {
                        nextind %= Order.Units.Count;
                    }
                    else
                        return null;
                }

                return new ControlUnitContainer(Order, nextind);
            }
            else if (distance < 0)
            {
                int beforeind = Index - (-distance);
                if (beforeind < 0)
                {
                    if (Order.IsRepeatable)
                    {
                        beforeind += Order.Units.Count;
                    }
                    else
                        return null;
                }

                return new ControlUnitContainer(Order, beforeind);
            }
            else
                return this;
        }

        public int GetDistanceOfBlockedUnit(int limit)
        {
            ControlUnitContainer container = this.Next;
            for (int i = 1; i <= limit; ++i)
            {
                if (container == null || container.Unit.IsBlocked)
                    return i;
                else
                    container = container.Next;
            }
            return limit;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            throw new NotImplementedException();
            return base.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Index.GetHashCode() ^ Order.GetHashCode();
        }

        public int BlockPassedCount
        {
            get
            {
                if (this.Order.IsRepeatable)
                {
                    return int.MaxValue;
                }
                else
                {
                    return this.Index;
                }
            }
        }
    }

    /// <summary>
    ///     列車が運行する経路を保持するクラス．
    ///     与えられたBlockの配列を，運行に必要なBlockを加味しながら，シーケンシャルに確保・解放する
    /// </summary>
    [DataContract]
    public class Route
    {
        private readonly RouteOrder _routeOrder;
        private IEnumerator<ControlUnitContainer> _order_enmtor;

        [DataMember]
        public IEnumerable<ControlUnit> LockedUnits
        {
            get
            {
                var controlUnitContainer = this.ReservedUnit;
                if (controlUnitContainer != null)
                    return this.PassedUnits.Concat(new[] {  controlUnitContainer.Unit });
                else
                    return this.PassedUnits;
            }
        }

        [DataMember]
        public Queue<ControlUnit> PassedUnits { get; private set; }

        [DataMember]
        //先頭の一個先をリザーブ
        public ControlUnitContainer ReservedUnit { get; private set; }

        [DataMember]
        public bool IsReserving { get; private set; }

        [DataMember(IsRequired = false)]
        public ICollection<Block> LockedBlocks
        {
            get
            {
                if (LockedUnits == null)
                    return new Block[] { };

                return LockedUnits.ToArray().SelectMany(u => u.Blocks).ToList();
            }
        }

        public ControlUnit CenterUnit
        {
            get
            {
                int len = this.PassedUnits.Count;

                if (len <= 0) // 確保していなければnull
                    return null;
                else
                    return this.PassedUnits.Last();
            }
        }

        public ControlUnitContainer HeadContainer
        {
            get { return this._order_enmtor.Current; }
        }

        [DataMember]
        public RouteOrder RouteOrder
        {
            get { return _routeOrder; }
            set { throw new NotImplementedException(); }
        }

        #region Initialize

        public Route(RouteOrder order)
        {
            _routeOrder = order;
            this.PassedUnits = new Queue<ControlUnit>();
            //this.ind_current = -1;

        }

        public void InitLockingPosition()
        {
            while (ReleaseLastUnit()) ;
        }

        private void initEnumerator(ControlUnit unit)
        {
            if (_order_enmtor != null)
            {
                InitLockingPosition();
                _order_enmtor.Dispose();
                _order_enmtor = null;
            }

            _order_enmtor = RouteOrder.Enumerate(unit).GetEnumerator();
            _order_enmtor.MoveNext();
            allocateCurrent();
        }

        #endregion

        #region Lock Methods

        /// <summary>
        ///     次のControlUnitをロック．戻り値は成功したかどうか
        /// </summary>
        /// <returns>ロックできたならばTrue．失敗した場合はFalse</returns>
        public bool LockNextUnit()
        {
            if (_order_enmtor == null)
                throw new InvalidOperationException("ロックできる状態にありません．AllocateTrainが呼び出されているか確認してください");

            ControlUnitContainer next = _order_enmtor.Current.Next;
            if (next != null && next.Unit.CanBeAllocated)
            {
                bool movecond = _order_enmtor.MoveNext();
                if (!movecond)
                    return false;

                allocateCurrent();
                return true;
            }
            else
                return false;
        }

        private void allocateCurrent()
        {
            _order_enmtor.Current.Unit.Allocate();
            this.PassedUnits.Enqueue(_order_enmtor.Current.Unit);

            if (this.IsReserving)
            {
                this.ReservedUnit = this.HeadContainer.Next;
            }
        }

        public bool ReleaseLastUnit()
        {
            if (_order_enmtor == null)
                throw new InvalidOperationException("ロックできる状態にありません．AllocateTrainが呼び出されているか確認してください");

            if (this.PassedUnits.Count > 0)
            {
                ControlUnit deleted = this.PassedUnits.Dequeue();
                deleted.Release();

                return true;
            }
            return false;
        }

        public void ReserveHead()
        {
            if (_order_enmtor == null)
                throw new InvalidOperationException("ロックできる状態にありません．AllocateTrainが呼び出されているか確認してください");

            if (!this.IsReserving)
            {
                this.IsReserving = true;
                this.ReservedUnit = this.HeadContainer.Next;
            }
        }

        public void UnReserveHead()
        {
            if (_order_enmtor == null)
                throw new InvalidOperationException("ロックできる状態にありません．AllocateTrainが呼び出されているか確認してください");
            if (this.IsReserving)
            {
                this.IsReserving = false;
                this.ReservedUnit = null;
            }
        }

        #endregion

        public void LookUpTrain()
        {
            //while (!RouteOrder.IsRouteFinished)
            //{
            //    if (!RouteOrder.IsSectionFinished)
            //        this.LockNextUnit();
            //    else
            //        break;
            //}

            //while (RouteOrder.IsLeftSectionFirst)
            //    this.ReleaseLastUnit();
            throw new NotImplementedException();
        }

        public void AllocateTrain(Block headblock, int len)
        {
            if (len <= 0)
                throw new ArgumentOutOfRangeException("len must be greater than 0");

            ControlUnit unit = RouteOrder.SearchUnit(headblock);
            ControlUnitContainer container = RouteOrder.CreateContainer(unit);

            //get tail
            ControlUnitContainer tail = container;
            if (len > 1)
                tail = container.GetNeighbor(-(len - 1));

            initEnumerator(tail.Unit);
            for (int i = 1; i < len; ++i)
            {
                if (!LockNextUnit())
                    throw new InvalidOperationException("cannot allocate block");
            }
        }

        #region ToString

        public override string ToString()
        {
            return _routeOrder.ToString();
        }

        #endregion

        //public int IndCurrent
        //{
        //    set { ind_current = value; }
        //    get { return ind_current; }
        //}
    }
}