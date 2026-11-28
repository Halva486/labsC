using BusinessLayer;
using ServiceLayer;
namespace PresentationLayer;
public class Form1 : Form
{
    private readonly GiftService service=new();
    private readonly ListBox list=new(){Left=400,Top=30,Width=420,Height=350};
    private readonly TextBox name=new(){Left=160,Top=35,Width=200};
    private readonly TextBox weight=new(){Left=160,Top=80,Width=200};
    private readonly TextBox sugar=new(){Left=160,Top=125,Width=200};
    private readonly TextBox extra=new(){Left=160,Top=170,Width=200};
    private readonly ComboBox type=new(){Left=160,Top=215,Width=200,DropDownStyle=ComboBoxStyle.DropDownList};
    private readonly TextBox minSugar=new(){Left=160,Top=440,Width=80,Text="0"};
    private readonly TextBox maxSugar=new(){Left=260,Top=440,Width=80,Text="100"};
    private readonly Label total=new(){Left=400,Top=400,Width=300,Text="Общий вес: 0 г"};
    public Form1()
    {
        Text="Новогодний подарок"; Width=870; Height=570; StartPosition=FormStartPosition.CenterScreen;
        Controls.AddRange(new Control[]{list,total});
        AddField("Название:",name,35); AddField("Вес (г):",weight,80); AddField("Сахар (%):",sugar,125); AddField("Шоколад / вкус:",extra,170);
        type.Items.AddRange(new object[]{"Шоколадная","Карамельная"}); type.SelectedIndex=0;
        AddField("Тип конфеты:",type,215);
        AddButton("Добавить",160,270,AddSweet);
        AddButton("Сортировать по весу",260,270,()=>{service.SortByWeight();RefreshList();});
        AddButton("Найти по сахару",160,390,Search);
        Controls.Add(new Label{Left=30,Top=443,Width=120,Text="Диапазон сахара:"}); Controls.Add(minSugar); Controls.Add(maxSugar);
        AddButton("Показать все",260,480,RefreshList);
        AddButton("Сохранить",400,480,()=>{service.SaveGift();MessageBox.Show("Подарок сохранён.");});
        RefreshList();
    }
    private void AddField(string label,Control control,int y){Controls.Add(new Label{Left=30,Top=y+4,Width=125,Text=label});Controls.Add(control);}
    private void AddButton(string text,int x,int y,Action action){var b=new Button{Text=text,Left=x,Top=y,Width=180,Height=32};b.Click+=(s,e)=>action();Controls.Add(b);}
    private void AddSweet()
    {
        try {
            if(!double.TryParse(weight.Text,out var w)||!double.TryParse(sugar.Text,out var s)) throw new ArgumentException("Введите корректные числовые значения.");
            var sweet=type.SelectedIndex==0?(Sweet)new ChocolateCandy(name.Text,w,s,extra.Text):new CaramelCandy(name.Text,w,s,extra.Text);
            service.AddSweet(sweet); name.Clear();weight.Clear();sugar.Clear();extra.Clear();RefreshList();
        } catch(Exception ex){MessageBox.Show(ex.Message,"Ошибка");}
    }
    private void Search()
    {
        try {if(!double.TryParse(minSugar.Text,out var min)||!double.TryParse(maxSugar.Text,out var max))throw new ArgumentException("Введите диапазон сахара.");
            list.Items.Clear();foreach(var x in service.FindBySugarRange(min,max))list.Items.Add(x.ToString());
        }catch(Exception ex){MessageBox.Show(ex.Message,"Ошибка");}
    }
    private void RefreshList(){list.Items.Clear();foreach(var x in service.GetAllSweets())list.Items.Add(x.ToString());total.Text=$"Общий вес: {service.GetTotalWeight()} г";}
}