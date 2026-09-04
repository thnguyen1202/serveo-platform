// // 1. Cập nhật Props của CreateForm
// interface CreateFormProps extends React.ComponentProps<'form'> {
//   isPending?: boolean;
//   // Hoặc truyền trực tiếp onSubmit nếu mutation thực thi ở cha
//   // onSubmit?: (data: FormData) => void; 
// }

// export function CreateForm({ className, isPending, ...props }: CreateFormProps) {
//   return (
//     <Form form={form} onSubmit={form.handleSubmit(onSubmit)} className={cn(className)} {...props}>
//       {/* Các field ở đây */}
//     </Form>
//   );
// }

// // 2. Sử dụng ở Component cha
// export function ParentComponent() {
//   const loginMutation = useLogin();

//   return (
//     <div>
//       <CreateForm isPending={loginMutation.isPending} />
      
//       {/* Nút bấm hoặc UI ở ngoài có thể dùng isPending dễ dàng */}
//       <Button disabled={loginMutation.isPending}>
//         {loginMutation.isPending ? 'Processing...' : 'Action'}
//       </Button>
//     </div>
//   );
// }