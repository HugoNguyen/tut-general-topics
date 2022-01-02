import { GraphQLScalarType } from "graphql";
import { ClassType, Field, Int, ObjectType } from "type-graphql";

export function BaseEdge<TItem>(TItemClass: ClassType<TItem>) {
    @ObjectType({isAbstract: true})
    abstract class BaseEdgeClass {
        @Field(() => TItemClass)
        node: TItem;
        @Field(() => Int)
        cursor: number;
    }

    return BaseEdgeClass;
}

export function BasePageInfo<TKeyId>(TKeyId: ClassType<TKeyId> | GraphQLScalarType | String | Number | Boolean) {
    @ObjectType({isAbstract: true})
    abstract class BasePageInfoClass {
        @Field(() => TKeyId)
        endCursor: TKeyId;
        @Field()
        hasNextPage: boolean;
    }

    return BasePageInfoClass;
}

export function BasePaginatedResponse<TEdge, TPageInfo>(
    TEdgeClass: ClassType<TEdge>,
    TPageInfoClass: ClassType<TPageInfo>,
) {
    @ObjectType({ isAbstract: true })
    abstract class BasePaginatedResponseClass {
        @Field()
        totalCount: number;
        @Field(() => [TEdgeClass])
        edges: [TEdge];
        @Field(() => TPageInfoClass)
        pageInfo: TPageInfo;
    }
    return BasePaginatedResponseClass;
}
