export interface ApiProblemDetailsBase {
  type?: string;
  title?: string;
  status: number;
  detail?: string;
  instance?: string;
  traceId?: string;
}

export interface ApiProblemError {
  code: string;
  message: string;
  type: string;
  target?: string | null;
}

export interface ValidationProblemDetails extends ApiProblemDetailsBase {
  status: 400;
  errors: Record<string, string[]>;
}

export interface ErrorProblemDetails extends ApiProblemDetailsBase {
  status: 401 | 403 | 404 | 409 | 422 | 500;
  errors: ApiProblemError[];
}

export type ApiProblemDetails = ErrorProblemDetails | ValidationProblemDetails;
