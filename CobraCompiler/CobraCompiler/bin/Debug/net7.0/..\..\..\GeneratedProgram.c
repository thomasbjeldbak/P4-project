#include <stdio.h>
#include <stdlib.h>
struct node
{
 int value;
 struct node *next;
};
void AddToList (struct node **list, int n){
 struct node *new_node = malloc(sizeof(struct node));
 new_node->value = n;
 new_node->next = NULL;
 if (*list == NULL) {
 *list = new_node;
 } else {
 struct node *last_node = *list;
 while (last_node->next != NULL) {
 last_node = last_node->next;
 }
 last_node->next = new_node;
 }
};
void ReplaceInList(struct node *list, int index, int value)
{
 struct node *curr_node = list;
 int i;
 for (i = 0; i < index; i++)
 { curr_node = curr_node->next; }
 curr_node->value = value;
}
int IndexOfList(struct node *list, int value)
{
 struct node *curr_node = list;
 int index = 0;
 while (curr_node != NULL) {
 if (curr_node->value == value)
 { return index; }
 curr_node = curr_node->next;
 index++;
 }
 return -1;
}
int ValueOfList(struct node *list, int index)
{
 struct node *curr_node = list;
 int i;
 for (i = 0; i < index; i++)
 { curr_node = curr_node->next; }
 return curr_node->value;
}
void main(){
int hello = 10;
}
