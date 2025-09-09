#include <iostream>
#include <time.h>
#include <stdlib.h>
using namespace std;

int main()
{
    int a = 8;
    int *w;
    int b = 16;
    int *w1;

    w=&a;
    w1=&b;

    cout << a << endl;
    cout << w << endl;
    cout << *w << endl;
    cout << endl;
    *w=24;

    cout << a << endl;
    cout << w << endl;
    cout << *w << endl;
    cout << endl;

    cout << b << endl;
    cout << w1 << endl;
    cout << *w1 << endl;
    cout << endl;
    cout << *w+*w1 << endl;

    a=55;
    cout << endl;
    cout << *w << endl;
    cout << endl;

    srand(time(NULL));
    int tab[10];
    for (int i = 0; i < 10; i++){
        tab[i]=rand()%90+10;

        cout << tab[i] << " ";
    }
    cout << endl;
    int* tabPtr1 = tab;
    for (int i = 0; i < 10; i++){
        *tabPtr1 = rand()%900+100;

        tabPtr1 = tabPtr1 + 1;

        cout << tab[i] << " ";
    }
    cout<<endl;
    int* dynamicTab = new int[20];
    for (int i = 0; i < 20; i++){
        dynamicTab[i] = rand()%10+1;
        cout<<dynamicTab[i]<<" ";
    }
    cout<<endl;
    delete(dynamicTab);

    int** tab2 = new int*[6];
    cout<<endl<<"Po stworzeniu"<<endl;
    for (int i = 0; i < 6; i++){
        tab2[i] = new int[3];
        for (int j = 0; j < 3; j++){
            tab2[i][j] = rand()%10+1;
            cout<<tab2[i][j]<< " ";
        }
        cout<<endl;
    }
    int min = tab2[0][0];
    for (int i = 0; i < 6; i++){
        for (int j = 0; i < 6; i++){
            if (tab2[i][j] < min)
                min = tab2[i][j];
        }
    }
    cout<<"najmniejszy element tablicy to: "<<min<<endl<<endl;

    for (int i = 0; i < 6; i++) {
        delete(tab2[i]);
    }
    delete(tab2);
    cout<<"Po usunieciu"<<endl;
    for (int i = 0; i < 6; i++){
        for (int j = 0; j < 3; j++){
            cout<<tab2[i][j]<< " ";
        }
        cout<<endl;
    }
    return 0;
}
