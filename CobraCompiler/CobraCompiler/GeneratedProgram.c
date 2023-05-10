#include <stdio.h>
#include <stdlib.h>
#include <string.h>
struct node
{
 void *value;
 struct node *next;
};
void AddToList (struct node **list, void *value, size_t value_size){
 struct node *new_node = malloc(sizeof(struct node));
 new_node->value = malloc(value_size);
 memcpy(new_node->value, value, value_size);
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
void ReplaceInList(struct node *list, int index, void *value)
{
 struct node *curr_node = list;
 int i;
 for (i = 0; i < index; i++)
 { curr_node = curr_node->next; }
 curr_node->value = value;
}
int IndexOfList(struct node *list, void *value)
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
void *ValueOfList(struct node *list, int index)
{
 struct node *curr_node = list;
 int i;
 for (i = 0; i < index; i++)
 { curr_node = curr_node->next; }
 return curr_node->value;
}
void printList(struct node *hej){
{
char *h;int number = 1;
while (number)
{
h = *(char **)hej->value;
printf("%s\n", h);
if (hej->next == NULL)
{
number = 0;
} else
{
hej = hej->next;
}
}
}
}
void main(){
struct node *hej = NULL;
AddToList(&hej, &(char *){"1"}, sizeof(char *));AddToList(&hej, &(char *){"2"}, sizeof(char *));AddToList(&hej, &(char *){"3"}, sizeof(char *));AddToList(&hej, &(char *){"4"}, sizeof(char *));AddToList(&hej, &(char *){"5"}, sizeof(char *));printList(hej);
}
