//Задача 62: Заполните спирально массив 4 на 4(массив заполняется по часовой стрелке от периферии к центру).

// Сделаем чтобы можно было задать матрицу любого размера, чтобы можно было выбрать угол,
// из которого будет начинаться спираль, и чтобы можно было выбрать направление спирали (по или против часовой)

// Можно было бы еще сделать направление заматывания или разматывания к/от центра, 
// но меня чего-то на это не хватило...

using System;

void PrintMatrix(int [,] local_matrix)  // красивая печать матрицы
{
    for(int i=0;i<local_matrix.GetLength(0);i++)
    {
        Console.Write("[");  // каждую строку начинаем печатать с символа [
        for(int j=0;j<local_matrix.GetLength(1)-1;j++)
        {
            Console.Write($"{local_matrix[i,j],3} ");  // печатаем очередной элемент матрицы, с 2-мя знаками
                                                        // после запятой и пробелом после всего числа
        }
        Console.WriteLine($"{local_matrix[i,local_matrix.GetLength(1)-1],3}]"); 
                                    //печатаем поcледний элемент в строке и символ ]
    }    
}

// функция заполняет периметр на заданной глубине, начиная из angle угла и в rotate сторону
// angle    0 1 // угол - из которого начинается спираль
//          3 2
// rotate = true - по часовой стрелке, иначе - против часовой стрелки
void FillPerimetr(int depth,int angle,bool rotate,int [,] lm)
{  
    int [] n = new int [4];  // тут храним текущие вычисленные точки во 4-м направлениям
    for(int i = depth; i<lm.GetLength(0)-1-depth;i++) // вычисляем, сколько точек надо обойти 
                                                      // на заданной "глубине"
    {
        n[0] = i+1+((lm.GetLength(0)-depth)*4*depth-depth);
        n[1] = lm.GetLength(0)+i+((lm.GetLength(0)-depth)*4*depth-depth*3);
        n[2] = lm.GetLength(0)*2+i-1+((lm.GetLength(0)-depth)*4*depth-depth*5);
        n[3] = lm.GetLength(0)*3+i-2+((lm.GetLength(0)-depth)*4*depth-depth*7);

        if (rotate)  // заполняем в нужную сторону на нужный угол уже вычисленные значения
        {
            lm[depth,i] = n[(0+4-angle)%4];
            lm[i,lm.GetLength(0)-1-depth] = n[(1+4-angle)%4];
            lm[lm.GetLength(0)-1-depth,lm.GetLength(0)-1-i] = n[(2+4-angle)%4];
            lm[lm.GetLength(0)-1-i,depth] = n[(3+4-angle)%4];
        }
        else
        {
            lm[i,depth] = n[(0+angle)%4];
            lm[lm.GetLength(0)-1-depth,i] = n[(1+angle)%4];
            lm[lm.GetLength(0)-1-i,lm.GetLength(0)-1-depth] = n[(2+angle)%4];
            lm[depth,lm.GetLength(0)-1-i] = n[(3+angle)%4];
        }
    }
    if (lm.GetLength(0)/2 == depth) // последняя точка в матрице с нечетым количеством строк
    {
        lm[lm.GetLength(0)/2,lm.GetLength(0)/2]=lm.GetLength(0)*lm.GetLength(0);
    }
}

int [,] FillMatrix(int sizeN,int angle,bool rotate) // заполняем матрицу сходящимися к центру "кольцами"
{
    if (sizeN<2)
    {
        Console.WriteLine("Ошибка! Слишком маленькая матрица!");
        System.Environment.Exit(-1);
    }
    if (!((angle>=1)&&(angle<=3)))
    {
        Console.WriteLine("Ошибка! Неверно задан угол поворота! Задайте угол = [0..3]");
        System.Environment.Exit(-1);
    }
    int [,] ret = new int [sizeN,sizeN];
    for(int i=0;i<=(ret.GetLength(0)-1)/2;i++)  // i - "глубина" кольца от края
        FillPerimetr(i,angle,rotate,ret);       // заполняем "кольцо" с нужным направлением и поворотом 
    return ret;
}

// Программа
PrintMatrix(FillMatrix(15,3,false));

