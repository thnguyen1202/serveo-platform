import axios from 'axios';

import { ApiException } from '@/core/errors/api-exception';

export function handleHttpError(error: unknown) {
  // if (import.meta.env.VITE_ENABLE_LOG) {
  //   console.dir(error);
  // }

  if (axios.isAxiosError(error)) {
    const problem = error.response?.data;
    if (problem) {
      return new ApiException(problem, error.status);
    }
  }

  return error;
}

// export function handleHttpError(error: unknown) {
//   // console.log('constructor:', (error as object).constructor.name);
//   // console.log(JSON.stringify(error, null, 2));
//   if (axios.isAxiosError(error)) {
//     const problem = error.response?.data;
//     if (problem?.status) {
//       throw new ApiException(problem);
//     }
//   }

//   throw error;
// }
