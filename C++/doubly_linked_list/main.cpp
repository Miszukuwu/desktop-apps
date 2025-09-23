#include <iostream>

using namespace std;

struct Student {
    string firstName;
    int heightCM;
    Student *next, *previous;
};

void printListA(Student* head) {
    Student* temp = head;
    cout<<"Lista uczniow: "<<endl;
    while (temp!=NULL){
        cout<<temp->firstName<<" - "<<temp->heightCM<<" cm"<<endl;
        temp = temp->next;
    }
}
int main() {
    Student* head = NULL;
    do {
        Student* student = new Student();
        cout<<"Podaj imie: ";
        cin>>student->firstName;
        cout<<"Podaj wzrost (cm): ";
        cin>>student->heightCM;
        if (head==NULL) {
            student->next = NULL;
            student->previous = NULL;
            head = student;
        } else {
            Student* temp = head;
            while (temp->next!=NULL) {
                temp = temp->next;
            }
            student->previous = temp;
            temp->next = student;
        }
        char choice;
        cout<<"Czy chcesz wstawic kolejnego ucznia? (t/n): ";
        cin>>choice;
        if (choice!='t') {
            break;
        }
    } while (true);
    printListA(head);
    cout<<"Uczniowie ktorzy sa wyzszi od sasiadow: "<<endl;
    Student* temp = head;
    while (temp!=NULL){
        if ( (temp->previous == NULL || temp->heightCM > temp->previous->heightCM) &&
             (temp->next == NULL || temp->heightCM > temp->next->heightCM)) {
            cout<<temp->firstName<<" - "<<temp->heightCM<<endl;
        }
        temp = temp->next;
    }
    return 0;
}
