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
int mod(int a, int b)
{
int remainder = a;
while((remainder >= b))
{
remainder = (remainder - b);
}
return remainder;}
int GetMax(int input_a, int input_b)
{
if((input_a > input_b))
{
return input_a;}
else
{
return input_b;}
return -1;}
int GetMin(int input_a, int input_b)
{
if((input_a < input_b))
{
return input_a;}
else
{
return input_b;}
return -1;}
void main(){
int divisor = 0;
int input_a = 0;
int input_b = 0;
int min = 0;
int max = 0;
int remainder = 0;
char * tryAgain = "y";
while(0 == strcmp(tryAgain, "y"))
{
printf("%s\n", "Enter two positive integers:");
int continue_ = 0;
while((continue_ == 0))
{
input_a = *(int *)input("%d", sizeof(int));
input_b = *(int *)input("%d", sizeof(int));
if(((input_a <= 0) || (input_b <= 0)))
{
printf("%s\n", "Please enter two positive integers:");
continue_ = 0;
}
else
{
continue_ = 1;
}
}
max = GetMax(input_a, input_b);
min = GetMin(input_a, input_b);
continue_ = 0;
int i = min;
while(((i > 0) && (continue_ == 0)))
{
if(((mod(max, i) == 0) && (mod(min, i) == 0)))
{
divisor = i;
continue_ = 1;
}
i = (i - 1);
}
printf("%s\n", "The biggest divisor is:");
printf("%d\n", divisor);
printf("%s\n", "Would you like to try again? (y/n)");
tryAgain = (char *)input("%s", sizeof(char *));
}
}
