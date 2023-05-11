#include <stdio.h>
#include <stdlib.h>
#include <string.h>
char* concat(const char *str1, const char *str2) {
 size_t len1 = strlen(str1);
 size_t len2 = strlen(str2);
 char *result = malloc(strlen(str1) + strlen(str2) + 1);
 strcpy(result, str1);
 strcat(result, str2);
 return result;
}
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
float triArea(float width, float height)
{
return ((width * height) / 2);
}
void main(){
printf("%g\n", triArea(2, 3));
printf("%g\n", triArea(7, 4));
printf("%g\n", triArea(10, 10));
printf("%g\n", triArea(3, 3));
}
