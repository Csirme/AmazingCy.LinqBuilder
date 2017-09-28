# AmazingCy.LinqBuilder
To Easy Builder Linq  Like Mongdo .Net Driver.
方便实现动态拼接Linq语句.实现Linq to Sql的调用


//Eg:
//条件1
var fTime= AmazingCy.LinqBuilder.Builder<Mint_Moment>.Create(c =>
                    c.UpdateTime <= fetch.SpiltTime && c.DataStatus == (int) DataStatus.Success);
//条件2
var fDataStatus = AmazingCy.LinqBuilder.Builder<Mint_Moment>.Create(
                   c => c.DataStatus == (int)DataStatus.Success);
//AND 拼接两个条件
 
 var filter = fTime & fDataStatus;
 
 //使用查询
 vara Count = db.Mint_Moments.Count(filter.Expression);
 
