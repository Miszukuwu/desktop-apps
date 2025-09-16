#include <iostream>

using namespace std;

struct Item{
    string name;
    float price;

    Item* next;
};

Item* deleteAtIndex(Item* head, int index){
    Item* toDelete = head;
    Item* previous = NULL;
    for (int i = 0; i < index; i++){
        previous = toDelete;
        toDelete = toDelete->next;
        if (toDelete==NULL){
            return head;
        }
    }
    if (previous!=NULL)
        previous->next = toDelete->next;
    Item* next = toDelete->next;
    delete(toDelete);
    return next;
}

int main()
{
    Item* head = NULL;
    Item* tail = new Item;
    tail->next = NULL;
    while (true){
        Item* item = new Item;
        string name; float price;
        cout<<"Podaj nazwe przedmiotu: ";
        cin>>name;
        cout<<"Podaj cene przedmiotu: ";
        cin>>price;
        item->name = name;
        item->price = price;
        item->next = NULL;
        tail->next = item;
        tail = item;
        if (head==NULL){
            head = item;
        }
        cout<<"Czy chcesz wprowadzic nastepny przedmiot do listy (t/n): ";
        char choice;
        cin>>choice;
        if (choice!='t'){
            break;
        }
    }

    Item* item = head;
    int lp = 0;
    cout<<"Lista przedmiotow: "<<endl;
    while (item != NULL){
        cout<<lp<<". "<<item->name<<" - "<<item->price<<" zl"<<endl;
        item = item->next;
        lp++;
    }
    while (true){
        int indexToDelete;
        cout<<"Ktory element chcesz usunac: "<<endl;
        cin>>indexToDelete;
        if (indexToDelete==0){
            head = deleteAtIndex(head, indexToDelete);
        } else {
            deleteAtIndex(head, indexToDelete);
        }
        item = head;
        lp = 0;
        cout<<"Lista przedmiotow po usunieciu elementu nr ("<<indexToDelete<<"): "<<endl;
        while (item != NULL){
            cout<<lp<<". "<<item->name<<" - "<<item->price<<" zl"<<endl;
            item = item->next;
            lp++;
        }
    }
    return 0;
}
