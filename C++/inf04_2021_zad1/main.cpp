#include <iostream>
#include <time.h>
#include <stdlib.h>

using namespace std;

const int M = 10;

class ArrayHelper{
    private:
        int* arr1;
        /************************************************

         * nazwa funkcji:       findMaxValueIndex

         * parametry wej�ciowe: arr - tablic� w kt�rej jest szukany index
                                N - wielko�� tablicy arr
                                startFrom - index od kt�rego jest rozpocz�te szukanie

         * warto�� zwracana:    int - index maksymalnej warto�ci w tablicy

         * autor:               000000000
        *************************************************/
        int findMaxValueIndex(int* arr,int N, int startFrom){
            int maxValue = arr[startFrom];
            int maxIndex = startFrom;
            for (int j = startFrom; j < N; j++) {
                if (arr[j] > maxValue){
                    maxValue = arr[j];
                    maxIndex = j;
                }
            }
            return maxIndex;
        }
    public:
        void populateArray(){
            arr1 = new int[M];
            cout<<"Wprowad� "<<M<<" wartosci: "<<endl;
            srand(time(NULL));
            for (int i = 0; i < M; i++){
                arr1[i] = rand()%19+1;
                //cin>>arr1[i];
            }
        }
        /************************************************

         * nazwa funkcji:       sortArray

         * parametry wej�ciowe: arr - tablic� kt�ra jest sortowana malej�co
                                N - wielko�� tablicy arr

         * warto�� zwracana:    brak

         * autor:               000000000
        *************************************************/
        void sortArrayR(int* arr, int N){
            for (int i = 0; i < N; i++) {
                int maxValueIndex = findMaxValueIndex(arr, N, i);
                int temp = arr[i];
                arr[i] = arr[maxValueIndex];
                arr[maxValueIndex] = temp;
            }
        }
        void printArray(int* arr, int N) {
            for (int i = 0; i < N; i++) {
                cout<<arr[i]<<" ";
            }
            cout<<endl;
        }
        int* getArray(){
            return arr1;
        }
};

int main()
{
    ArrayHelper arrayHelper;
    arrayHelper.populateArray();
    arrayHelper.printArray(arrayHelper.getArray(), M);
    arrayHelper.sortArrayR(arrayHelper.getArray(), M);
    arrayHelper.printArray(arrayHelper.getArray(), M);
    return 0;
}
