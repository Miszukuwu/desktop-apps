#include <iostream>

using namespace std;

struct Osoba {
    string nazwisko;
    int wiek;

    Osoba* next;
};

int main() {
    Osoba* head = NULL;
    char d;
    while (true){
        Osoba* nowaOsoba;
        try {
            nowaOsoba = new Osoba;
        } catch (bad_alloc){
            cout<<"Nie ma ju¿ miejsca na nastêpny elemnt"<<endl;
            break;
        }
        cout<<"Podaj nazwisko: ";
        cin>>nowaOsoba->nazwisko;
        cout<<"Podaj wiek: ";
        cin>>nowaOsoba->wiek;
        nowaOsoba->next = head;
        head = nowaOsoba;
        cout<<"Kolejny rekord? t/n:";
        cin>>d;
        if (d=='n' || d!='t'){
            break;
        }
    }
    Osoba* current = head;
    do {
        cout << current->nazwisko <<" ma "<<current->wiek<<" lat"<<endl;
        current = current->next;
    } while (current != NULL);
    return 0;
}
