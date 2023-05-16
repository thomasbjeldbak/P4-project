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
void* input(char* format, size_t size) {
 void* input = malloc(size);
 int result = scanf(format, input);
 if (result != 1) {
 fprintf(stderr, "Error: Invalid input format\n");
 exit(EXIT_FAILURE);
 }
 return input;
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
void ReplaceInList(struct node *list, void *value, int index)
{
 struct node *curr_node = list;
 int i;
 for (i = 0; i < index; i++) {
 if (curr_node == NULL) {
 fprintf(stderr, "Error: Invalid index\n");
 exit(EXIT_FAILURE);
 }
 curr_node = curr_node->next;
 }
 if (curr_node == NULL) {
 fprintf(stderr, "Error: Invalid index\n");
 exit(EXIT_FAILURE);
 }
 curr_node->value = value;
}
int IndexOfList(struct node *list, void *value, size_t value_size)
{
 struct node *curr_node = list;
 int index = 0;
 while (curr_node != NULL) {
 if (memcmp(curr_node->value, value, value_size) == 0)
 { return index; }
 curr_node = curr_node->next;
 index++;
 }
 fprintf(stderr, "Error: Value not found in list\n");
 exit(EXIT_FAILURE);
}
void *ValueOfList(struct node *list, int index)
{
 struct node *curr_node = list;
 int i;
 for (i = 0; i < index; i++) {
 if (curr_node == NULL) {
 fprintf(stderr, "Error: Invalid index\n");
 exit(EXIT_FAILURE);
 }
 curr_node = curr_node->next;
 }
 if (curr_node == NULL) {
 fprintf(stderr, "Error: Invalid index\n");
 exit(EXIT_FAILURE);
 }
 return curr_node->value;
}
void print_(int y)
{
printf("%d\n", y);
}
void main(){
int y = 10;
printf("%d\n", y);
if(1)
{
print_(y);
printf("%d\n", y);
int y_ = 12;
print_(y);
printf("%d\n", y_);
if(1)
{
print_(y);
printf("%d\n", y_);
int y__ = 15;
print_(y);
printf("%d\n", y__);
}
}
print_(y);
printf("%d\n", y);
}
